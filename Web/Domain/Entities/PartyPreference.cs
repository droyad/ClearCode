namespace ClearCode.Data.Entities
{
    public class PartyPreference
    {
        public Candidate Candidate { get;  }
        public int Ordinal { get;}
        public string Pref { get; }
        public int Year { get;  }

        public PartyPreference(string candidate, int ordinal, string pref)
        {
            Candidate = new Candidate(candidate);
            Ordinal = ordinal;
            Pref = pref;
            Year = 2016;
        }
    }
}