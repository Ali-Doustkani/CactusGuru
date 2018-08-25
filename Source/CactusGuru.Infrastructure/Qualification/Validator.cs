using System.Collections.Generic;

namespace CactusGuru.Infrastructure.Qualification
{
    public class Validator<T> : ValidatorBase<T>
        where T : DomainEntity
    {
        public Validator()
        {
            _specs = new List<ISpecification>();
        }

        private readonly List<ISpecification> _specs;

        protected override ErrorCollection ValidateImp(T domainEntity)
        {
            var result = new ErrorCollection();
            foreach (var spec in _specs)
                result.Add(spec.Satisfy(domainEntity));
            return result;
        }

        public static Validator<T> Create(params ISpecification[] specs)
        {
            var ret = new Validator<T>();
            foreach (var spec in specs)
                ret._specs.Add(spec);
            return ret;
        }
    }
}
