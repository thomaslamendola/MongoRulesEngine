using RulesEngine.Models;
using System.Collections.Generic;

namespace RulesEngine
{
    public static class Extensions
    {
        public static List<Rule> FilterByTags(this List<Rule> rules, List<string> tagKeys)
        {
            var result = new List<Rule>();
            foreach (var rule in rules)
            {
                var keep = true;
                var ruleTagKeys = rule.Tags?.Keys;
                if (ruleTagKeys != null)
                {
                    foreach (var ruleTagKey in ruleTagKeys)
                    {
                        if (!tagKeys.Contains(ruleTagKey))
                        {
                            keep = false;
                            break;
                        }
                    }
                }
                if (keep)
                {
                    result.Add(rule);
                }
            }
            return result;
        }

        public static List<Rule> FilterByMatch(this List<Rule> rules, IDictionary<string, object> tags)
        {
            var result = new List<Rule>();
            foreach (var rule in rules)
            {
                var keep = true;
                foreach (var tag in tags)
                {
                    var currentTagKey = tag.Key;
                    if (rule.Tags != null)
                    {
                        if (rule.Tags.ContainsKey(currentTagKey))
                        {
                            var expectedValue = tag.Value;
                            var relatedRuleValue = rule.Tags[currentTagKey];
                            if (!relatedRuleValue.Equals(expectedValue))
                            {
                                keep = false;
                                break;
                            }
                        }
                    }
                }
                if (keep)
                {
                    result.Add(rule);
                }
            }
            return result;
        }
    }
}
