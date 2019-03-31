using RulesEngine.Models;
using System.Collections.Generic;
using System.Linq;

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

        public static List<Rule> FilterByMatch(this List<Rule> rules, IDictionary<string, string> tags, IDictionary<string, List<string>> dynamicDatasets)
        {
            var result = new List<Rule>();
            foreach (var rule in rules)
            {
                var keep = true;
                foreach (var tag in tags)
                {
                    var currentTagKey = tag.Key;
                    if (rule.Tags.ContainsKey(currentTagKey))
                    {
                        var expectedValue = tag.Value;
                        var ruleTags = rule.Tags.ToDictionaryStringListOfStrings();

                        var relatedRuleValues = ruleTags[currentTagKey];

                        var placeholder = relatedRuleValues.Where(t => t.Contains("%")).FirstOrDefault();
                        if (!string.IsNullOrWhiteSpace(placeholder))
                        {
                            var trimmedPlaceholder = placeholder.Replace("%", "");
                            if (dynamicDatasets.ContainsKey(trimmedPlaceholder))
                            {
                                if (!dynamicDatasets[trimmedPlaceholder].Contains(expectedValue))
                                {
                                    keep = false;
                                    break;
                                }
                            }
                        }
                        else if (!relatedRuleValues.Contains(expectedValue))
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

        public static Dictionary<string, List<string>> ToDictionaryStringListOfStrings(this IDictionary<string, object> dict)
        {
            var ruleTags = dict.ToDictionary(t => t.Key, t => (List<object>)t.Value);
            var castedRuleTags = new Dictionary<string, List<string>>();

            foreach (var ruleTag in ruleTags)
            {
                var castedList = ruleTag.Value.OfType<string>();
                castedRuleTags.Add(ruleTag.Key, castedList.ToList());
            }
            return castedRuleTags;
        }
    }
}
