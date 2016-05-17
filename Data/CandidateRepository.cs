using System.Collections.Generic;
using System.Linq;
using Autofac;
using ClearCode.Data.Entities;

namespace ClearCode.Data
{
    [InstancePerDependency]
    public class CandidateRepository : ICandidateRepository
    {
        private readonly IDataContext _dataContext;

        public CandidateRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public List<Candidate> Candidates => _dataContext.Table<Candidate>().ToList();
    }
}