using System;
using System.Collections.Generic;
using RulesEngine.Models;

namespace RulesEngine.Repositories
{
    public class RulesRepository : IRulesRepository
    {
        public IEnumerable<Rule> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Rule> GetByContext(Context context)
        {
            
        }

        public bool Remove(string id)
        {
            throw new NotImplementedException();
        }

        public bool Set(Rule rule)
        {
            throw new NotImplementedException();
        }

        public bool Update(Rule rule)
        {
            throw new NotImplementedException();
        }
    }
}
