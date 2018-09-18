using CactusGuru.Domain.Persistance.Repositories;
using CactusGuru.Infrastructure.Qualification;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CactusGuru.Domain.Greenhouse.Qualification.Inquiries
{
    public class TaxonInquiry : InquiryBase<Taxon>
    {
        public TaxonInquiry(ICollectionItemRepository repo)
        {
            _repo = repo;
        }

        private readonly ICollectionItemRepository _repo;

        protected override ErrorCollection InquiryImp(Guid id)
        {
            var ret = new ErrorCollection();
            var collectionItemErrors = InquiryCollectionItems(id);
            if (!string.IsNullOrEmpty(collectionItemErrors))
                ret.Add(collectionItemErrors);
            return ret;
        }

        private string InquiryCollectionItems(Guid taxonId)
        {
            var itemTitles = new List<string>();
            var items = _repo.GetByTaxonId(taxonId);
            if (!items.Any()) return string.Empty;
            foreach (var collectionItem in items)
                itemTitles.Add( collectionItem.Format("{code} - {GENUS} {taxon}"));
            return string.Format("This taxon is used in the following collection items: {0}{1}",
                                 Environment.NewLine,
                                 string.Join(Environment.NewLine, itemTitles));
        }
    }
}
