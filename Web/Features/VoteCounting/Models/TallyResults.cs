using System.Collections.Generic;

namespace ClearCode.Web.Features.VoteCounting.Models
{
    public class TallyResults
    {
        public List<Dictionary<string, int>>  Counts { get; set; }
    }
}