using MongoDB.Bson;
using RulesEngine.Models;
using System.Collections.Generic;

namespace RulesEngine.Repositories
{
    public interface IRulesRepository
    {
        IEnumerable<Rule> GetRulesByKey(string key);
        IEnumerable<Rule> GetRulesByKeyAndContext(string key, Context context);
        IEnumerable<string> GetValuesByKeyAndContext(string key, Context context);
        IEnumerable<Rule> GetAll();
        Rule Set(Rule rule);
        bool Remove(ObjectId id);
        bool RemoveAll();
        bool Update(Rule rule);
    }
}