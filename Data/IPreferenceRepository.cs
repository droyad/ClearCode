using System.Collections.Generic;

namespace ClearCode.Data
{
    public interface IPreferenceRepository
    {
        Dictionary<string, string[]> GetPartyPreferences(int year);
    }
}