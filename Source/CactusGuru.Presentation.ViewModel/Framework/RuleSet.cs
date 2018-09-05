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

            public IRuleSet ValidatesForItself(Func<string> action)
            {
                _rules.Add(RuleType.Custom, new Tuple<Func<string>, string>(action, PropertyName));
                return this;
            }

            public IRuleSet ValidatesForWhole(Func<string> action)
            {
                _rules.Add(RuleType.Custom, new Tuple<Func<string>, string>(action, string.Empty));
                return this;
            }

            public IRuleSet ValidatesFor(string theOtherProperty, Func<string> action)
            {
                _rules.Add(RuleType.Custom, new Tuple<Func<string>, string>(action, theOtherProperty));
                return this;
            }

            public void Run(Dictionary<string, string> errors, object value, Action<string> eventRaiser)
            {
                string result = null;
                string eventTarget = null;
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
                        eventTarget = rule.Value.Item2;
                    }
                }
                if (eventTarget != null)
                    eventRaiser(eventTarget);
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
