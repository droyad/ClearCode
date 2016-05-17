using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using ClearCode.Services;
using ClearCode.Web.Features.VoteCounting;
using ClearCode.Web.Models;

namespace ClearCode.Web.Controllers
{
    public class VoteCountingController : Controller
    {
        private readonly IVoteCounter _voteCounter;

        public VoteCountingController(IVoteCounter voteCounter)
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
                var input = VoteInputParser.ParseInput(model.Votes);
                var results = _voteCounter.Tally(input);
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