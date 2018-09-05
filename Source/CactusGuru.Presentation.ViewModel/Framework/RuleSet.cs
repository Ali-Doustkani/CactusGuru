using System;
using System.Collections.Generic;
using System.Linq;

namespace CactusGuru.Presentation.ViewModel.Framework
{
    public partial class Rules
    {
        private class RuleSet : IRuleSet
        {
            private enum RuleType
            {
                Empty,
                Custom
            }

            private const string EMPTY_ERROR = "این فیلد نمی تواند خالی باشد";

            public RuleSet(string propName)
            {
                PropertyName = propName;
                _rules = new Dictionary<RuleType, Tuple<Func<string>, string>>();
            }

            public readonly string PropertyName;
            private readonly Dictionary<RuleType, Tuple<Func<string>, string>> _rules;

            public IRuleSet IsNotEmpty()
            {
                _rules.Add(RuleType.Empty, null);
                return this;
            }

            public IRuleSet ValidatesForWhole(Func<string> action)
            {
                _rules.Add(RuleType.Custom, new Tuple<Func<string>, string>(action, string.Empty));
                return this;
            }

            public void Run(Dictionary<string, string> errors, object value, Action<string> eventRaiser)
            {
                string result = null;
                var customRan = false;
                foreach (var rule in _rules)
                {
                    if (rule.Key == RuleType.Empty)
                    {
                        result = CheckEmpty(value);
                        errors[PropertyName] = result;
                    }
                    else if (rule.Key == RuleType.Custom)
                    {
                        result = rule.Value.Item1();
                        errors[rule.Value.Item2] = result;
                        customRan = true;
                    }
                }
                if (customRan)
                    eventRaiser(string.Empty);
            }

            public bool HasAnyCustom()
            {
                return _rules.Any(x => x.Key == RuleType.Custom);
            }

            private string CheckEmpty(object value)
            {
                if (value == null)
                    return EMPTY_ERROR;
                var str = value as string;
                if (str == string.Empty)
                    return EMPTY_ERROR;
                return null;
            }
        }
    }
}
