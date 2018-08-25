using System;

namespace CactusGuru.Infrastructure.Qualification
{
    public abstract class InquiryBase<T>
        where T : DomainEntity
    {
        public void Inquiry(Guid id)
        {
            Error.ThrowExceptionIf(InquiryImp(id));
        }

        protected abstract ErrorCollection InquiryImp(Guid id);
    }
}
