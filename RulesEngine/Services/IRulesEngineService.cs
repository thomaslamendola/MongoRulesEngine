using RulesEngine.Models;
using System.Collections.Generic;

namespace RulesEngine.Services
{
    public interface IRulesEngineService
    {
        IEnumerable<Rule> GetByContext(Context context);
    }
}