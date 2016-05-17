using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using ClearCode.Web.Domain;
using ClearCode.Web.Domain.Entities;
using ClearCode.Web.Features.VoteCounting.Models;
using ClearCode.Web.Plumbing;
using ClearCode.Web.Plumbing.Query;

namespace ClearCode.Web.Features.VoteCounting
{
    public class PartyPreferencesByYearFilter : IFilter<PartyPreference>
    {
        private readonly int _year;

        public PartyPreferencesByYearFilter(int year)
        {
            _year = year;
        }

        public IQueryable<PartyPreference> Execute(IQueryable<PartyPreference> items) => items.Where(p => p.Year == _year);
    }

    public class ProjectPartyPreferencesToLookupProjection :
        IScalarProjection<PartyPreference, Dictionary<String, String[]>>
    {
        public Dictionary<string, string[]> Execute(IQueryable<PartyPreference> items)
        {
            return items.GroupBy(p => p.Candidate)
                .ToDictionary(g => g.Key.Name,
                    g => new[] { g.Key.Name }.Concat(g.OrderBy(p => p.Ordinal).Select(p => p.Pref)).ToArray());
        }
    }

    [InstancePerDependency]
    public class VoteCounter
    {
        private readonly IQueryExecuter _queryExecuter;

        public VoteCounter(IQueryExecuter queryExecuter)
        {
            _queryExecuter = queryExecuter;
        }

        public Result<TallyResults> Tally(string[][] votes)
        {
            var tallyResults = new TallyResults
            {
                Counts = new List<Dictionary<string, int>>()
            };

            var partyPreferencesQ = new PartyPreferencesByYearFilter(2016)
                .Pipe(new ProjectPartyPreferencesToLookupProjection());
            var partyPreferences = _queryExecuter.Execute(partyPreferencesQ);


            var candidates = _queryExecuter.Execute(new AllFilter<Candidate>());
            var tally = candidates.ToDictionary(c => c.Name, c => new List<string[]>());

            IReadOnlyList<string[]> votesToDistribute = votes;
            while (true)
            {
                var result = DistributeVotes(votesToDistribute, partyPreferences, tally);
                if(result.WasFailure)
                    return Result<TallyResults>.Failed(result);

                tallyResults.Counts.Add(tally.ToDictionary(x => x.Key, x => x.Value.Count));

                if (tally.Count == 2)
                    break;

                var lowest = tally.OrderBy(r => r.Value.Count).First();
                tally.Remove(lowest.Key);
                votesToDistribute = lowest.Value;
            }
            return tallyResults;
        }

        public Result<TallyResults> Tally(string rawInput)
        {
            var votes = VoteInputParser.ParseInput(rawInput);
            return votes.WasFailure ? Result<TallyResults>.Failed(votes) : Tally(votes);
        }

        private static IResult DistributeVotes(IReadOnlyList<string[]> votes, Dictionary<string, string[]> partyPreferences, Dictionary<string, List<string[]>> tally)
        {
            var results = votes.Select((vote,n) =>
            {
                var preferences = GetPreferences(votes[n], partyPreferences, n);
                if (preferences.WasFailure)
                    return (IResult) preferences;

                var candidate = preferences.Value.FirstOrDefault(p => tally.Keys.Contains(p));

                if (candidate == null)
                    return Result.Failed($"{n}: No more preferences left");

                tally[candidate].Add(preferences);
                return Result.Success();
            }
            ) ;

            return Result.From(results.ToArray());
        }


        private static Result<string[]> GetPreferences(string[] vote, Dictionary<string, string[]> partyPreferences, int voteId)
        {
            if (vote.Length > 1)
                return vote;

            string[] preferences;
            if (!partyPreferences.TryGetValue(vote[0], out preferences))
                return Result<string[]>.Failed($"{voteId}: Could not find the party preferences for {vote[0]}");
            return preferences;
        }
    }

}