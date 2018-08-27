using CactusGuru.Domain.Persistance.Repositories;
using CactusGuru.Infrastructure.Persistance;
using CactusGuru.Infrastructure.Qualification;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CactusGuru.Domain.Greenhouse.Qualification.Inquiries
{
    public class SupplierInquiry : InquiryBase<Supplier>
    {
        public SupplierInquiry(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private readonly IUnitOfWork _unitOfWork;

        protected override ErrorCollection InquiryImp(Guid supplierId)
        {
            var ret = new ErrorCollection();
            var collectionItemError = InquiryCollectionItems(supplierId);
            if (!collectionItemError.Equals(Error.Empty))
                ret.Add(collectionItemError);
            return ret;
        }

        private Error InquiryCollectionItems(Guid supplierId)
        {
            var itemTitles = new List<string>();
            var items = _unitOfWork.CreateRepository<ICollectionItemRepository>().GetBySupplierId(supplierId);
            if (!items.Any()) return Error.Empty;
            foreach (var collectionItem in items)
                itemTitles.Add(collectionItem.Format("{code} - {GENUS} {taxon}"));
            return new Error($"تامین کننده ی مورد نظر در تعریف آیتم های ذیل استفاده شده است: {Environment.NewLine}{string.Join(Environment.NewLine, itemTitles)}");
        }
    }
}
