using System;
using System.Collections.Generic;
using System.Linq;
using CactusGuru.Domain.Greenhouse.Formatting;
using CactusGuru.Domain.Persistance.Repositories;
using CactusGuru.Infrastructure.Persistance;
using CactusGuru.Infrastructure.Qualification;

namespace CactusGuru.Domain.Greenhouse.Qualification.Inquiries
{
    public class CollectorInquiry : InquiryBase<Collector>
    {
        public CollectorInquiry(IUnitOfWork unitOfWork, IFormatter<CollectionItem> formatter)
        {
            _unitOfWork = unitOfWork;
            _formatter = formatter;
        }

        private readonly IUnitOfWork _unitOfWork;
        private readonly IFormatter<CollectionItem> _formatter;

        protected override ErrorCollection InquiryImp(Guid id)
        {
            var ret = new ErrorCollection ();
            var collectionItemError = InquiryCollectionItems(id);
            if (!collectionItemError.Equals(Error.Empty))
                ret.Add(collectionItemError);
            return ret;
        }

        private Error InquiryCollectionItems(Guid collectorId)
        {
            var taxonTitles = new List<string>();
            var collectionItems = _unitOfWork.CreateRepository<ICollectionItemRepository>().GetByCollectorId(collectorId);
            if (!collectionItems.Any()) return Error.Empty;
            foreach (var item in collectionItems)
                taxonTitles.Add(_formatter.Format(item));
            return new Error($"کلکتور مورد نظر در تعریف اقلام های ذیل در مجموعه استفاده شده است: {Environment.NewLine}{string.Join(Environment.NewLine, taxonTitles)}");
        }
    }
}
