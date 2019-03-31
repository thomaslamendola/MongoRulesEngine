using MongoDB.Bson;
using MongoDB.Driver;
using RulesEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RulesEngine.Repositories
{
    public class RulesRepository : IRulesRepository
    {
        private IMongoCollection<Rule> _collection;

        public RulesRepository(IMongoCollection<Rule> collection)
        {
            _collection = collection;

            var ruleIndexBuilder = Builders<Rule>.IndexKeys;
            var indexModel = new CreateIndexModel<Rule>(ruleIndexBuilder.Ascending(r => r.Key));
            _collection.Indexes.CreateOne(indexModel);
        }

        public IEnumerable<Rule> GetAll()
        {
            return _collection.Find(_ => true).ToList();
        }

        public IEnumerable<Rule> GetRulesByKey(string key)
        {
            return _collection.Find(r => r.Key.Equals(key)).ToList();
        }

        public IEnumerable<Rule> GetRulesByKeyAndContext(string key, Context context)
        {
            var result = new List<Rule>();
            var rules = _collection.Find(r => r.Key.Equals(key)).ToList();

            var relevantRulesByTags = new List<Rule>();
            var tagKeys = context.Tags.GroupBy(t => t.Key).Select(g => g.Key).ToList();

            rules = rules.FilterByTags(tagKeys);
            rules = rules.FilterByMatch(context.Tags, context.DynamicDatasets);

            var defaultRules = rules.Where(r => r.Type.Equals(RuleType.Default)).ToList();
            var replacementRules = rules.Where(r => r.Type.Equals(RuleType.Replacement)).ToList();

            foreach (var replacementRule in replacementRules)
            {
                var ruleToRemove = defaultRules.Where(r => r.Value.Equals(replacementRule.ValueToBeReplaced)).FirstOrDefault(); //giving for granted the value is unique... otherwise all should be removed (if needed according to replacement rule)
                if (ruleToRemove != null)
                {
                    defaultRules.Remove(ruleToRemove);
                    defaultRules.Add(replacementRule);
                }
            }

            result.AddRange(defaultRules);

            return result;
        }

        public IEnumerable<string> GetValuesByKeyAndContext(string key, Context context)
        {
            return GetRulesByKeyAndContext(key, context).Select(r => r.Value).Distinct().ToList();
        }

        public bool Remove(ObjectId id)
        {
            return _collection.DeleteOne(r => r.Id.Equals(id)).IsAcknowledged;
        }

        public Rule Set(Rule rule)
        {
            try
            {
                _collection.InsertOne(rule);
            }
            catch (Exception)
            {
                return null;
            }

            return rule;
        }

        public bool Update(Rule rule)
        {
            return _collection.ReplaceOne(r => r.Id.Equals(rule.Id), rule).IsAcknowledged;
        }

        public bool RemoveAll()
        {
            return _collection.DeleteMany(_ => true).IsAcknowledged;
        }
    }
}
