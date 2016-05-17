using System.Collections.Generic;
using ClearCode.Data.Entities;

namespace ClearCode.Data
{
    public interface ICandidateRepository
    {
        List<Candidate> Candidates { get; }
    }
}