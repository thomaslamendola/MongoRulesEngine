using MongoDB.Driver;
using RulesEngine.Models;
using RulesEngine.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
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
            var collection = db.GetCollection<Rule>("rules");

            _repository = new RulesRepository(collection);
        }

        [Fact]
        public void Test1()
        {
            var context = new Context();

            context.Tags.Add("country", "CA");
            context.Tags.Add("language", "en");
            context.Tags.Add("companyCode", "BRE");
            context.Tags.Add("brandName", "jackpotcity");
            context.Tags.Add("platform", "Desktop");
            context.Tags.Add("product", "Casino");

            var result = _repository.GetByKeyAndContext("contentareaid", context);
            Assert.NotEmpty(result);

        }
    }
}
