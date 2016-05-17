using System.Collections.Generic;
using System.Linq;
using Autofac;
using ClearCode.Data.Entities;

namespace ClearCode.Data
{
    [InstancePerDependency]
    public class PreferenceRepository : IPreferenceRepository
    {
        private readonly IDataContext _context;

        public PreferenceRepository(IDataContext context)
        {
            _context = context;
        }

        public Dictionary<string, string[]> GetPartyPreferences(int year)
        {
            return _context.Table<PartyPreference>()
                .Where(p => p.Year == year)
                .GroupBy(p => p.Candidate)
                .ToDictionary(g => g.Key.Name, g => new [] { g.Key.Name }.Concat(g.OrderBy(p => p.Ordinal).Select(p => p.Pref)).ToArray());
        }
    }
}