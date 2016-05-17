using System;
using System.Configuration;
using System.Linq;
using ClearCode.Web.Plumbing;

namespace ClearCode.Web.Features.VoteCounting
{
    public class VoteInputParser
    {
        public static Result<string[][]> ParseInput(string votes)
        {
            var input = votes.Split('\n')
                .Select(v => v.Split(',').Select(p => p.Trim()).ToArray())
                .ToArray();

            var maxNumberOfPreferences = int.Parse(ConfigurationManager.AppSettings["MaximumNumberOfPreferences"]);
            if (input.Any(v => v.Length > maxNumberOfPreferences))
            {
                return Result<string[][]>.Failed("One or more votes has more than the maximum number of allowed preferences");
            }
            return input;
        }
    }
}