using RulesEngine.Models;
using System.Collections.Generic;

namespace RulesEngine.Repositories
{
    public interface IRulesRepository
    {
        IDictionary<string, Rule> GetByContext(Context context);
        IEnumerable<Rule> GetAll();
        bool Set(Rule rule);
        bool Remove(string id);
        bool Update(Rule rule);
    }
}