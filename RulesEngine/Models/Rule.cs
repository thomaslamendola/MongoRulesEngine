using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RulesEngine.Models
{
    public class Rule
    {
        private int _priority;

        [BsonId]
        public string Id { get; set; } 
        public RuleType Type { get; set; }

        public string Key { get; set; } // GAMESCATEGORYID
        public string Value { get; set; } //SLOTS

        [BsonExtraElements]
        public IDictionary<string, object> Tags { get; set; }

        public int Priority
        {
            get => Tags?.Count ?? 0;
            set => _priority = Tags?.Count ?? 0;
        }
    }

    public enum RuleType
    {
        Default,
        Replacement
    }
}
