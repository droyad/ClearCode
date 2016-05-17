using System.Collections.Generic;
using System.Linq;
using ClearCode.Web.Domain.Entities;
using ClearCode.Web.Plumbing.Query;

namespace ClearCode.Web.Features.VoteCounting
{
    public class ProjectPartyPreferencesToLookupProjection :
        IScalarProjection<PartyPreference, Dictionary<Candidate, Preferences>>
    {
        public Dictionary<Candidate, Preferences> Execute(IQueryable<PartyPreference> items)
        {
            return items.GroupBy(p => p.Candidate)
                .ToDictionary(g => g.Key,g => new Preferences(g.Key, g));
        }
    }
}