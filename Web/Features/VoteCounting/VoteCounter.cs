using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using ClearCode.Data;
using ClearCode.Data.Entities;
using ClearCode.Services;

namespace ClearCode.Web.Features.VoteCounting
{
    [InstancePerDependency]
    public class VoteCounter : IVoteCounter
    {
        private readonly IDataContext _dataContext;

        public VoteCounter(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Results Tally(string[][] votes)
        {
            var results = new Results
            {
                Counts = new List<Dictionary<string, int>>()
            };

            var partyPreferences = _dataContext.Table<PartyPreference>()
                .Where(p => p.Year == 2016)
                .GroupBy(p => p.Candidate)
                .ToDictionary(g => g.Key.Name, g => new[] { g.Key.Name }.Concat(g.OrderBy(p => p.Ordinal).Select(p => p.Pref)).ToArray());

            var candidates = _dataContext.Table<Candidate>().ToList();
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

    public interface IVoteCounter
    {
        Results Tally(string[][] input);
    }
}