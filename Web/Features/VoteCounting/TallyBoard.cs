using System.Collections.Generic;
using System.Linq;
using ClearCode.Web.Domain.Entities;
using ClearCode.Web.Features.VoteCounting.Models;
using ClearCode.Web.Plumbing;

namespace ClearCode.Web.Features.VoteCounting
{
    public class TallyBoard
    {
        private readonly Dictionary<Candidate, List<Vote>> _tally;
        private int _round;

        private TallyBoard(IReadOnlyList<Candidate> candidates)
        {
            _tally = candidates.ToDictionary(c => c, c => new List<Vote>());
        }

        private int RemainingCandidates => _tally.Keys.Count;
        private bool WinnersKnown => RemainingCandidates <= 2;

        private void DistributeVotes(IReadOnlyList<Vote> votes)
        {
            foreach (var vote in votes)
            {
                var candidate = vote.GetFirstPreferenceFrom(_tally.Keys);
                _tally[candidate].Add(vote);
            }
        }

        private TallyResult GetCurrentResult()
        {
            return new TallyResult(
                _round,
                _tally.Select(c => new CandidateResult(c.Key, c.Value.Count)).ToArray()
                );
        }

        private void InitalCount(IReadOnlyList<Vote> votes)
        {
            _round = 1;
            DistributeVotes(votes);
        }

        private void EliminateAndRedistribute()
        {
            _round++;
            var lowest = _tally.OrderBy(x => x.Value.Count).First();
            _tally.Remove(lowest.Key);
            DistributeVotes(lowest.Value);
        }

        public static IReadOnlyList<TallyResult> Tally(IReadOnlyList<Candidate> candidates, IReadOnlyList<Vote> votes)
        {
            var results = new List<TallyResult>();

            var board = new TallyBoard(candidates);

            board.InitalCount(votes);
            results.Add(board.GetCurrentResult());
            while (!board.WinnersKnown)
            {
                board.EliminateAndRedistribute();
                results.Add(board.GetCurrentResult());
            }
            return results;
        }
    }
}