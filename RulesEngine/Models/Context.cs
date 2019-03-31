using System;
using System.Collections.Generic;
using System.Text;

namespace RulesEngine.Models
{
    public class Context
    {
        public Context()
        {
            Tags = new Dictionary<string, string>();
            DynamicDatasets = new Dictionary<string, List<string>>();
        }

        public IDictionary<string, string> Tags { get; set; }
        public IDictionary<string, List<string>> DynamicDatasets { get; set; }
    }
}
