namespace CactusGuru.Infrastructure.Qualification
{
    public abstract class ValidatorBase<T>
        where T : DomainEntity
    {
        public void Validate(T domainEntity)
        {
            var errors = ValidateImp(domainEntity);
            Error.ThrowExceptionIf(errors);
        }

        protected abstract ErrorCollection ValidateImp(T domainEntity);

        private static ValidatorBase<T> _empty;
        public static ValidatorBase<T> Empty
        {
            get
            {
                if (_empty == null)
                    _empty = new NullValidator<T>();
                return _empty;
            }
        }

        private class NullValidator<T> : ValidatorBase<T>
            where T : DomainEntity
        {
            protected override ErrorCollection ValidateImp(T domainEntity)
            {
                return new ErrorCollection();
            }
        }
    }
}