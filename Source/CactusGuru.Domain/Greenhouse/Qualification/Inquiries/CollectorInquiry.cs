using CactusGuru.Domain.Persistance.Repositories;
using CactusGuru.Infrastructure.Qualification;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CactusGuru.Domain.Greenhouse.Qualification.Inquiries
{
    public class CollectorInquiry : InquiryBase<Collector>
    {
        public CollectorInquiry(ICollectionItemRepository repo)
        {
            _repo = repo;
        }

        private readonly ICollectionItemRepository _repo;

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
            var collectionItems = _repo.GetByCollectorId(collectorId);
            if (!collectionItems.Any()) return Error.Empty;
            foreach (var item in collectionItems)
                taxonTitles.Add(item.Format("{code} - {GENUS} {taxon}"));
            return new Error($"کلکتور مورد نظر در تعریف اقلام های ذیل در مجموعه استفاده شده است: {Environment.NewLine}{string.Join(Environment.NewLine, taxonTitles)}");
        }
    }
}
