using System;
using System.Collections.Generic;
using CactusGuru.Infrastructure.Persistance;
using CactusGuru.Infrastructure.Utils;

namespace CactusGuru.Domain.Greenhouse.Qualification.Inquiries
{
    public abstract class InquiryBase
    {
        public abstract IUnitOfWork UnitOfWork { get; set; }

        protected abstract IEnumerable<string> InquiryImp();

        public IEnumerable<string> Inquiry()
        {
            if (UnitOfWork == null)
                throw new InvalidOperationException("Class field can not be null.");

            var ret = new List<string>();
            ret.AddRange(InquiryImp());
            return ret;
        }

        public string InquiryMessage()
        {
            return string.Join(Environment.NewLine, Inquiry());
        }
    }
}
