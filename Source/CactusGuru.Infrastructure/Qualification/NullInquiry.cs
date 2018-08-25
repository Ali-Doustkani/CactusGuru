using System;

namespace CactusGuru.Infrastructure.Qualification
{
    public class NullInquiry<T> : InquiryBase<T>
        where T : DomainEntity
    {
        protected override ErrorCollection InquiryImp(Guid id)
        {
            return new ErrorCollection();
        }
    }
}
