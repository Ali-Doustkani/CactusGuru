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
    }
}