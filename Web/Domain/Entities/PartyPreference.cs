namespace ClearCode.Web.Domain.Entities
{
    public class PartyPreference
    {
        public Candidate Candidate { get;  }
        public int Ordinal { get;}
        public string Preferences { get; }
        public int Year { get;  }

        public PartyPreference(string candidate, int ordinal, string preferences)
        {
            Candidate = new Candidate(candidate);
            Ordinal = ordinal;
            Preferences = preferences;
            Year = 2016;
        }
    }
}