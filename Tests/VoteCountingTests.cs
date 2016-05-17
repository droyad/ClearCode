using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ClearCode.Web.Controllers;
using ClearCode.Web.Features.VoteCounting;
using ClearCode.Web.Features.VoteCounting.Models;
using ClearCode.Web.Models;
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
            var controller = CreateTestController();
            var model = new VoteCountingIndexModel()
            {
                Votes = string.Join(Environment.NewLine, testData.Select(d => string.Join(", ", d)))
            };
            var view = (ViewResult)controller.Index(model);
            var result = (Results) view.Model;

            var winners = result.Counts
                .Last()
                .OrderByDescending(x => x.Value)
                .ToArray();

            Assert.AreEqual(winners[0].Key, "Rudd");
            Assert.AreEqual(winners[1].Key, "Stott Despoja");
            Assert.AreEqual(winners[0].Value, 5421);
            Assert.AreEqual(winners[1].Value, 4579);
        }

        [TestMethod]
        public void TooManyPreferencesTest()
        {
            var controller = CreateTestController();
            var model = new VoteCountingIndexModel()
            {
                Votes = "A,B,C,D,E,F,G"
            };
            var view = (ViewResult)controller.Index(model);
            var result = (VoteCountingIndexModel)view.Model;
            Assert.AreEqual(result.Error, "One or more votes has more than the maximum number of allowed preferences");
        }


        private static VoteCountingController CreateTestController()
        {
            var controller =
                new VoteCountingController(new VoteCounter(new FakeDataContext()));
            return controller;
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
