using System.Collections.Generic;
using System.Linq;
using ClearCode.Web.Domain.Entities;
using ClearCode.Web.Plumbing;

namespace ClearCode.Web.Features.VoteCounting
{
    public class Preferences : ReadOnlyList<Candidate>
    {

        public Preferences(IEnumerable<string> preferences) : base(preferences.Select(p => new Candidate(p)))
        {
        }

        public Preferences(Candidate candidate, IEnumerable<PartyPreference> partyPreferences)
            : base(new[] { candidate }.Concat(partyPreferences.Select(p => new Candidate(p.Preferences))))
        {
        }
    }
}