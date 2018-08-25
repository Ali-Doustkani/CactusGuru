using System;
using System.Collections.Generic;
using System.Linq;
using CactusGuru.Domain.Greenhouse.Formatting;
using CactusGuru.Domain.Persistance.Repositories;
using CactusGuru.Infrastructure.Persistance;
using CactusGuru.Infrastructure.Qualification;

namespace CactusGuru.Domain.Greenhouse.Qualification.Inquiries
{
    public class TaxonInquiry : InquiryBase<Taxon>
    {
        public TaxonInquiry(IUnitOfWork uow, IFormatter<CollectionItem> formatter)
        {
            _uow = uow;
            _formatter = formatter;
        }

        private readonly IUnitOfWork _uow;
        private readonly IFormatter<CollectionItem> _formatter;

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
            var items = _uow.CreateRepository<ICollectionItemRepository>().GetByTaxonId(taxonId);
            if (!items.Any()) return string.Empty;
            foreach (var collectionItem in items)
                itemTitles.Add(_formatter.Format(collectionItem));
            return string.Format("تاکسون مورد نظر در تعریف آیتم های ذیل استفاده شده است: {0}{1}",
                                 Environment.NewLine,
                                 string.Join(Environment.NewLine, itemTitles));
        }
    }
}
