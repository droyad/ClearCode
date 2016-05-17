namespace ClearCode.Data.Entities
{
    public class Candidate
    {
        public string Name { get; }

        public Candidate(string name)
        {
            Name = name;
        }

        protected bool Equals(Candidate other)
        {
            return string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Candidate) obj);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }
    }
}