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
        }

        public IEnumerable<Rule> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Rule> GetByKey(string key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Rule> GetByKeyAndContext(string key, Context context)
        {
            var result = new Dictionary<string, Rule>();
            var rules = _collection.Find(r => r.Key.Equals(key)).ToList();

            var relevantRulesByTags = new List<Rule>();
            var tagKeys = context.Tags.GroupBy(t => t.Key).Select(g => g.Key).ToList();

            rules = rules.FilterByTags(tagKeys);
            rules = rules.FilterByMatch(context.Tags);

            var defaultRules = rules.Where(r => r.Type.Equals(RuleType.Default)).ToList();
            var replacementRules = rules.Where(r => r.Type.Equals(RuleType.Replacement)).ToList();

            #region Reference
            /*
            var items = new List<Rule>();

            var replacementRules = _collection.Find(i => i.Type.Equals(RuleType.Replacement)).ToList();
            items.AddRange(replacementRules.OrderByDescending(i => i.Priority).ToList());

            var defaultRules = _collection.Find(i => i.Type.Equals(RuleType.Default)).ToList();
            items.AddRange(defaultRules.OrderByDescending(i => i.Priority).ToList());

            var keys = items.GroupBy(i => i.Key).Select(g => g.Key).ToList();

            var existingTags = new List<string>();
            var tagsCollections = items.GroupBy(i => i.Tags?.Keys).Select(g => g.Key).ToList();
            foreach (var tagCollection in tagsCollections)
            {
                if (tagCollection != null)
                {
                    existingTags.AddRange(tagCollection);
                }
            }
            existingTags = existingTags.Distinct().ToList();

            var commonTags = context.Tags.Select(x => x.Key).Intersect(existingTags);

            var result = new Dictionary<string, Rule>();

            if(!commonTags.Any())
            {
                items = items.Where(i => i.Priority.Equals(0)).ToList();
            }

            foreach (var key in keys)
            {
                foreach (var item in items.Where(i => i.Key.Equals(key)).ToList())
                {
                    if (result.ContainsKey(key))
                        break;

                    var equals = true;

                    foreach (var kvp in context.Tags)
                    {
                        var k = kvp.Key;
                        var v = kvp.Value;

                        if (item.Tags != null && item.Tags.ContainsKey(k))
                        {
                            if (!item.Tags[k].Equals(v))
                            {
                                equals = false;
                                break;
                            }
                        }
                    }

                    if (equals)
                        result.Add(key, item);

                }
            }
            */
            #endregion

            return rules;
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
