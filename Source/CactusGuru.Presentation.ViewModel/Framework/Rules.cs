using System;
using System.Collections.Generic;
using System.Linq;

namespace CactusGuru.Presentation.ViewModel.Framework
{
    public partial class Rules
    {
        public const string EMPTY_ERROR = "Can not be empty";

        public Rules(Action<string> eventRaiser)
        {
            _eventRaiser = eventRaiser;
            _errors = new Dictionary<string, string>();
            _rules = new List<RuleSet>();
        }

        private readonly Action<string> _eventRaiser;
        private readonly List<RuleSet> _rules;
        private readonly Dictionary<string, string> _errors;

        public IRuleSet MakeSure(string propName)
        {
            var rule = GetOrCreate(propName);
            _rules.Add(rule);
            return rule;
        }

        public void Check(string propName, object value)
        {
            var ruleSet = RulesOf(propName);
            ruleSet.Run(_errors, value, _eventRaiser);
        }

        public bool AnyError()
        {
            return _errors.Any(x => x.Value != null);
        }

        public IEnumerable<string> GetErrors(string propName)
        {
            if (propName == null) return Enumerable.Empty<string>();
            if (!_errors.ContainsKey(propName)) return Enumerable.Empty<string>();
            if (string.IsNullOrEmpty(_errors[propName])) return Enumerable.Empty<string>();
            return new string[] { _errors[propName] };
        }

        public void ClearErrors()
        {
            var properties = _errors.Select(x => x.Key).ToArray();
            _errors.Clear();
            foreach (var property in properties)
                _eventRaiser(property);
        }

        private RuleSet GetOrCreate(string propName)
        {
            var ret = RulesOf(propName);
            if (ret == null)
                ret = new RuleSet(propName);
            return ret;
        }

        private RuleSet RulesOf(string propName)
        {
            return _rules.SingleOrDefault(x => x.PropertyName == propName);
        }
    }
}
