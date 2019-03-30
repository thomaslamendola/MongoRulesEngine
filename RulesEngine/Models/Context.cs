using System;
using System.Collections.Generic;
using System.Text;

namespace RulesEngine.Models
{
    public class Context
    {
        public Context()
        {
            Tags = new Dictionary<string, object>();
        }

        public IDictionary<string, object> Tags { get; set; }
    }
}
