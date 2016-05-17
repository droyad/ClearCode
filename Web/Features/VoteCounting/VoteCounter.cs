using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using ClearCode.Web.Domain;
using ClearCode.Web.Domain.Entities;
using ClearCode.Web.Features.VoteCounting.Models;
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

        public Results Tally(string[][] votes)
        {
            var results = new Results
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
                DistributeVotes(votesToDistribute, partyPreferences, tally);

                results.Counts.Add(tally.ToDictionary(x => x.Key, x => x.Value.Count));

                if (tally.Count == 2)
                    break;

                var lowest = tally.OrderBy(r => r.Value.Count).First();
                tally.Remove(lowest.Key);
                votesToDistribute = lowest.Value;
            }
            return results;
        }

        public Results Tally(string rawInput)
        {
            return Tally(VoteInputParser.ParseInput(rawInput));
        }

        private static void DistributeVotes(IReadOnlyList<string[]> votes, Dictionary<string, string[]> partyPreferences, Dictionary<string, List<string[]>> results)
        {
            for (var x = 0; x < votes.Count; x++)
            {
                var preferences = GetPreferences(votes[x], partyPreferences, x);
                var candidate = preferences.FirstOrDefault(p => results.Keys.Contains(p));

                if (candidate == null)
                    throw new Exception($"{x}: No more preferences left");
                results[candidate].Add(preferences);
            }
        }


        private static string[] GetPreferences(string[] vote, Dictionary<string, string[]> partyPreferences, int voteId)
        {
            if (vote.Length > 1)
                return vote;

            string[] preferences;
            if (!partyPreferences.TryGetValue(vote[0], out preferences))
                throw new Exception($"{voteId}: Could not find the party preferences for {vote[0]}");
            return preferences;
        }
    }

}