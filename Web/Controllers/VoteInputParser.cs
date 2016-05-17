using System;
using System.Configuration;
using System.Linq;

namespace ClearCode.Web.Controllers
{
    public class VoteInputParser
    {
        public static string[][] ParseInput(string votes)
        {
            var input = votes.Split('\n')
                .Select(v => v.Split(',').Select(p => p.Trim()).ToArray())
                .ToArray();

            var maxNumberOfPreferences = int.Parse(ConfigurationManager.AppSettings["MaximumNumberOfPreferences"]);
            if (input.Any(v => v.Length > maxNumberOfPreferences))
            {
                throw new Exception("One or more votes has more than the maximum number of allowed preferences");
            }
            return input;
        }
    }
}