using System.Collections.Generic;
using System.Linq;
using Autofac;
using ClearCode.Web.Domain;
using ClearCode.Web.Domain.Entities;
using ClearCode.Web.Features.VoteCounting.Models;
using ClearCode.Web.Plumbing;
using ClearCode.Web.Plumbing.Query;

namespace ClearCode.Web.Features.VoteCounting
{
    [InstancePerDependency]
    public class VoteCounter
    {
        private readonly IDataContext _dataContext;
        private readonly IQueryExecuter _queryExecuter;

        public VoteCounter(IDataContext dataContext, IQueryExecuter queryExecuter)
        {
            _dataContext = dataContext;
            _queryExecuter = queryExecuter;
        }

        public Result<TallyResults> Tally(string rawInput)
        {
            var partyPreferences = _queryExecuter.Execute(
                new PartyPreferencesByYearFilter(2016)
                .Pipe(new ProjectPartyPreferencesToLookupProjection())
            );

            var votes = rawInput.Split('\n')
                .Select(SplitAndTrim)
                .Select((v, n) => Vote.Create(n, v, partyPreferences))
                .ToArray();

            if(votes.Any(v => v.WasFailure))
                return Result<TallyResults>.Failed(votes);

            return Tally(votes.Select(v => v.Value).ToArray());
        }

        private static string[] SplitAndTrim(string v) => v.Split(',').Select(p => p.Trim()).ToArray();

        public Result<TallyResults> Tally(IReadOnlyList<Vote> votes)
        {
            var candidates = _dataContext.Table<Candidate>().ToArray();

            var results = TallyBoard.Tally(candidates, votes);

            return new TallyResults(results);
        }
     
    }
}