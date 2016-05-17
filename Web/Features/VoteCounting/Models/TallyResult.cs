using System.Collections.Generic;

namespace ClearCode.Web.Features.VoteCounting.Models
{
    public class TallyResult
    {
        public int Round { get; set; }
        public IReadOnlyList<CandidateResult> CandidateResults { get; set; }

        public TallyResult(int round, IReadOnlyList<CandidateResult> candidateResults)
        {
            Round = round;
            CandidateResults = candidateResults;
        }


    }
}