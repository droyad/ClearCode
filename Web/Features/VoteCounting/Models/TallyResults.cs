using System.Collections.Generic;

namespace ClearCode.Web.Features.VoteCounting.Models
{
    public class TallyResults
    {
        public IReadOnlyList<TallyResult> Tallies { get; set; }

        public TallyResults(IReadOnlyList<TallyResult> tallies)
        {
            Tallies = tallies;
        }
    }
}