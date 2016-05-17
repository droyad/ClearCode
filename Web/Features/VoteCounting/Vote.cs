using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using ClearCode.Web.Domain.Entities;
using ClearCode.Web.Plumbing;

namespace ClearCode.Web.Features.VoteCounting
{
    public class Vote
    {
        public int Id { get; }
        public IReadOnlyList<string> CastVote { get; }
        public Preferences Preferences { get; }
        public bool HasSpecifiedPreferences { get; }

        private Vote(int id, string[] castVote, Preferences preferences, bool hasSpecifiedPreferences)
        {
            Id = id;
            CastVote = castVote;
            Preferences = preferences;
            HasSpecifiedPreferences = hasSpecifiedPreferences;
        }

        public static Result<Vote> Create(int id, string[] vote, Dictionary<Candidate, Preferences> partyPreferences)
        {
            const int maxNumberOfPreferences = 5;
            if (vote.Length > maxNumberOfPreferences)
               return Result<Vote>.Failed($"{id}: has more than the maximum number of allowed preferences") ;

            if (vote.Length > 1)
                return new Vote(id, vote, new Preferences(vote), true);

            Preferences preferences;
            if (!partyPreferences.TryGetValue(new Candidate(vote[0]), out preferences))
                return Result<Vote>.Failed($"{id}: Could not find the party preferences for {vote[0]}");

            return new Vote(id, vote, preferences, false);
        }

        public Result<Candidate> GetFirstPreferenceFrom(IEnumerable<Candidate> candidates)
        {
            var candidate = Preferences.FirstOrDefault(candidates.Contains);
            if (candidate == null)
                return Result<Candidate>.Failed($"{Id}: No more preferences left");
            return candidate;
        }
    }
}