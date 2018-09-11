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

        private static InquiryBase<T> _empty;
        public static InquiryBase<T> Empty
        {
            get
            {
                if (_empty == null)
                    _empty = new NullInquiry<T>();
                return _empty;
            }
        }

        public class NullInquiry<T> : InquiryBase<T>
        where T : DomainEntity
        {
            protected override ErrorCollection InquiryImp(Guid id)
            {
                return new ErrorCollection();
            }
        }
    }
}
