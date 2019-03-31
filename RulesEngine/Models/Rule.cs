using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RulesEngine.Models
{
    public class Rule
    {
        public Rule()
        {
            Tags = new Dictionary<string, object>();
        }

        private int _priority;

        [BsonId]
        public ObjectId Id { get; set; } 

        [BsonRepresentation(BsonType.String)]
        public RuleType Type { get; set; }

        public string Key { get; set; }
        public string Value { get; set; }
        public string ValueToBeReplaced { get; set; }
        public string Metadata { get; set; }

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
