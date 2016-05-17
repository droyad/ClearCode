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
            try
            {
                var results = _voteCounter.Tally(model.Votes);
                return View("Results", results);
            }
            catch (Exception ex)
            {
                model.Error = ex.Message;
                return View(model);
            }
        }
    }
}