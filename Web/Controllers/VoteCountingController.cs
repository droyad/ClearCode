using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using ClearCode.Services;
using ClearCode.Web.Models;

namespace ClearCode.Web.Controllers
{
    public class VoteCountingController : Controller
    {
        private readonly IVotesService _votesService;

        public VoteCountingController(IVotesService votesService)
        {
            _votesService = votesService;
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
                var input = model.Votes.Split('\n')
                    .Select(v => v.Split(',').Select(p => p.Trim()).ToArray())
                    .ToArray();

                var maxNumberOfPreferences = int.Parse(ConfigurationManager.AppSettings["MaximumNumberOfPreferences"]);
                if (input.Any(v => v.Length > maxNumberOfPreferences))
                {
                    model.Error = "One or more votes has more than the maximum number of allowed preferences";
                    return View(model);
                }

                var results = _votesService.Tally(input);
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