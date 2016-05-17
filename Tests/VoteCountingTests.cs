using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ClearCode.Web.Controllers;
using ClearCode.Web.Features.VoteCounting;
using ClearCode.Web.Features.VoteCounting.Models;
using ClearCode.Web.Models;
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

            var counter = new VoteCounter(new FakeDataContext());
            var result = counter.Tally(testData);

            var winners = result.Counts
                .Last()
                .OrderByDescending(x => x.Value)
                .ToArray();

            winners[0].Key.Should().Be("Rudd");
            winners[1].Key.Should().Be("Stott Despoja");
            winners[0].Value.Should().Be(5421);
            winners[1].Value.Should().Be(4579);
        }

        [TestMethod]
        public void TooManyPreferencesTest()
        {
            Action action = () => VoteInputParser.ParseInput("A,B,C,D,E,F,G");
            action.ShouldThrow<Exception>()
                .WithMessage("One or more votes has more than the maximum number of allowed preferences");
        }


        private IEnumerable<string[]> GetTestData()
        {
            var candidates = new[]
            {
                new {Name = "Palmer", Popularity = 50},
                new {Name = "Xenaphon", Popularity = 36},
                new {Name = "Stott Despoja", Popularity = 60},
                new {Name = "Rudd", Popularity = 62},
                new {Name = "Abbott", Popularity = 2},
            };

            var rnd = new Random(325346);
            while (true)
            {
                var numberOfChoices = rnd.Next(10) <= 7 ? candidates.Length : 1;
                var preferences = new string[numberOfChoices];
                for (int y = 0; y < numberOfChoices; y++)
                {
                    var possibleChoices = candidates.Where(c => !preferences.Contains(c.Name)).ToArray();
                    var totalPopularity = possibleChoices.Sum(c => c.Popularity);
                    var choiceNum = rnd.Next(totalPopularity);
                    preferences[y] = possibleChoices.SkipWhile(
                        c =>
                        {
                            choiceNum = choiceNum - c.Popularity;
                            return choiceNum > 0;
                        }
                    ).First().Name;
                }
                yield return preferences;
            }
        }
    }
}
