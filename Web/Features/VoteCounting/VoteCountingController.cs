using System;
using System.Web.Mvc;
using ClearCode.Web.Features.VoteCounting.Models;

namespace ClearCode.Web.Features.VoteCounting
{
    public class VoteCountingController : Controller
    {
        private readonly VoteCounter _voteCounter;

        public VoteCountingController(VoteCounter voteCounter)
        {
            _voteCounter = voteCounter;
        }

        public ActionResult Index()
        {
            return View(new VoteCountingIndexModel());
        }

        [HttpPost]
        public ActionResult Index(VoteCountingIndexModel model)
        {
            var results = _voteCounter.Tally(model.Votes);
            if (results.WasSuccessful)
                return View("Results", results.Value);

            model.Error = results.ErrorString;
            return View(model);

        }
    }
}