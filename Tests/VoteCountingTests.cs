using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ClearCode.Web.Controllers;
using ClearCode.Web.Domain.Entities;
using ClearCode.Web.Features.VoteCounting;
using ClearCode.Web.Features.VoteCounting.Models;
using ClearCode.Web.Models;
using ClearCode.Web.Plumbing.Query;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClearCode.Tests
{
    [TestClass]
    public class VoteCountingTests
    {
        [TestMethod]
        public void FullTallyTest()
        {
            var testData = GetTestData().Take(10000).ToArray();

            var result = TallyBoard.Tally(GetCandidates(), testData);
            var winners = result
                .Last()
                .CandidateResults
                .OrderByDescending(x => x.Votes)
                .ToArray();

            winners[0].Candidate.Name.Should().Be("Rudd");
            winners[1].Candidate.Name.Should().Be("Stott Despoja");
            winners[0].Votes.Should().Be(5421);
            winners[1].Votes.Should().Be(4579);
        }


        [TestMethod]
        public void TooManyPreferencesTest()
        {
            var result = Vote.Create(1, new string[6], GetPartyPreferences());
            result.WasSuccessful.Should().BeFalse();
            result.ErrorString.Should().Be("1: has more than the maximum number of allowed preferences");
        }


        private Candidate[] GetCandidates()
        {
            return new[]
            {
                new Candidate("Palmer"),
                new Candidate("Xenaphon"),
                new Candidate("Stott Despoja"),
                new Candidate("Rudd"),
                new Candidate("Abbott")
            };
        }


        private IEnumerable<Vote> GetTestData()
        {
            var candidates = new[]
            {
                new {Name = "Palmer", Popularity = 50},
                new {Name = "Xenaphon", Popularity = 36},
                new {Name = "Stott Despoja", Popularity = 60},
                new {Name = "Rudd", Popularity = 62},
                new {Name = "Abbott", Popularity = 2},
            };

            var partyPreferences = GetPartyPreferences();


            var rnd = new Random(325346);
            var id = 1;
            while (true)
            {
                var numberOfChoices = rnd.Next(10) <= 7 ? candidates.Length : 1;
                var castVote = new string[numberOfChoices];
                for (int y = 0; y < numberOfChoices; y++)
                {
                    var possibleChoices = candidates.Where(c => !castVote.Contains(c.Name)).ToArray();
                    var totalPopularity = possibleChoices.Sum(c => c.Popularity);
                    var choiceNum = rnd.Next(totalPopularity);
                    castVote[y] = possibleChoices.SkipWhile(
                        c =>
                        {
                            choiceNum = choiceNum - c.Popularity;
                            return choiceNum > 0;
                        }
                    ).First().Name;
                }
                yield return Vote.Create(id++, castVote, partyPreferences);
            }
        }

        private static Dictionary<Candidate, Preferences> GetPartyPreferences()
        {
            var partyPreferences = new Dictionary<Candidate, Preferences>()
            {
                {new Candidate("Palmer"), new Preferences(new[] {"Palmer", "Xenaphon", "Rudd", "Stott Despoja", "Abbott"})},
                {new Candidate("Xenaphon"), new Preferences(new[] { "Xenaphon", "Rudd", "Stott Despoja", "Abbott", "Palmer"})},
                {new Candidate("Stott Despoja"), new Preferences(new[] {"Stott Despoja", "Xenaphon", "Rudd", "Abbott", "Palmer"})},
                {new Candidate("Rudd"), new Preferences(new[] {"Rudd", "Xenaphon", "Palmer", "Stott Despoja", "Abbott"})},
                {new Candidate("Abbott"), new Preferences(new[] {"Abbott", "Xenaphon", "Rudd", "Stott Despoja", "Palmer"})}
            };
            return partyPreferences;
        }
    }
}
