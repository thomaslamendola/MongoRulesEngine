using MongoDB.Driver;
using RulesEngine.Models;
using RulesEngine.Repositories;
using System.Collections.Generic;
using Xunit;

namespace RulesEngine.Tests.Repositories
{
    public class TestRepositories
    {
        public IRulesRepository _repository { get; set; }

        public TestRepositories()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("rulesengine");
            var collection = db.GetCollection<Rule>("rulestest");

            _repository = new RulesRepository(collection);
        }

        [Fact]
        public void InsertDefaultRule()
        {
            var rule = new Rule
            {
                Key = "Test",
                Type = RuleType.Default,
                Value = "0000"
            };

            var result = _repository.Set(rule);
            Assert.True(result != null);
        }

        [Fact]
        public void InsertExtraRules()
        {
            var rule = new Rule
            {
                Key = "Test",
                Type = RuleType.Default,
                Value = "1BRE",
                Tags = new Dictionary<string, object>()
                {
                    { "companyCode", new List<string> { "BRE" } }
                }
            };
            _repository.Set(rule);

            rule = new Rule
            {
                Key = "Test",
                Type = RuleType.Default,
                Value = "1TPG",
                Tags = new Dictionary<string, object>()
                {
                    { "companyCode", new List<string> { "TPG" } }
                }
            };
            _repository.Set(rule);

            rule = new Rule
            {
                Key = "Test",
                Type = RuleType.Default,
                Value = "2BRE",
                Tags = new Dictionary<string, object>()
                {
                    { "language", new List<string> { "%swissLanguages%" } },
                    { "companyCode", new List<string> { "BRE" } }

                }
            };
            _repository.Set(rule);

            Assert.True(true);
        }

        [Fact]
        public void GetValuesByKeyAndContext()
        {
            var context = new Context();

            context.Tags.Add("country", "AU");
            context.Tags.Add("language", "en");
            context.Tags.Add("companyCode", "BRE");
            context.Tags.Add("brandName", "jackpotcity");
            context.Tags.Add("platform", "Mobile");
            context.Tags.Add("product", "Casino");

            context.DynamicDatasets.Add("swissLanguages", new List<string> { "it", "de", "fr" });

            var result = _repository.GetValuesByKeyAndContext("Test", context);

            Assert.NotEmpty(result);
        }

        [Fact]
        public void RemoveRule()
        {
            var rule = new Rule
            {
                Key = "Test",
                Type = RuleType.Default,
                Value = "XXXX"
            };
            rule = _repository.Set(rule);

            Assert.True(_repository.Remove(rule.Id));
        }

        [Fact]
        public void RemoveAllRules()
        {
            Assert.True(_repository.RemoveAll());
        }
    }
}
