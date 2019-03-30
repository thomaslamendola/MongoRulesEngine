using RulesEngine.Models;
using System.Collections.Generic;

namespace RulesEngine.Repositories
{
    public interface IRulesRepository
    {
        IEnumerable<Rule> GetByKey(string key);
        IEnumerable<Rule> GetByKeyAndContext(string key, Context context);
        IEnumerable<Rule> GetAll();
        bool Set(Rule rule);
        bool Remove(string id);
        bool Update(Rule rule);
    }
}