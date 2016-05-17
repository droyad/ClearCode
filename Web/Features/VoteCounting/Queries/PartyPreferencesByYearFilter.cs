using System.Linq;
using ClearCode.Web.Domain.Entities;
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
}