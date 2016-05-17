using ClearCode.Web.Domain.Entities;

namespace ClearCode.Web.Features.VoteCounting.Models
{
    public class CandidateResult
    {
        public Candidate Candidate { get; set; }
        public int Votes { get; set; }

        public CandidateResult(Candidate candidate, int votes)
        {
            Candidate = candidate;
            Votes = votes;
        }
    }
}