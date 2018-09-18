using CactusGuru.Domain.Persistance.Repositories;
using CactusGuru.Infrastructure.Qualification;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CactusGuru.Domain.Greenhouse.Qualification.Inquiries
{
    public class GeneraInquiry : InquiryBase<Genus>
    {
        public GeneraInquiry(ITaxonRepository repo)
        {
            _repo = repo;
        }

        private readonly ITaxonRepository _repo;

        protected override ErrorCollection InquiryImp(Guid id)
        {
            var ret = new ErrorCollection();
            var collectionItemError = InquiryTaxa(id);
            if (!collectionItemError.Equals(Error.Empty))
                ret.Add(collectionItemError);
            return ret;
        }

        private Error InquiryTaxa(Guid genusId)
        {
            var taxonTitles = new List<string>();
            var taxa = _repo.GetByGeneraId(genusId);
            if (!taxa.Any()) return Error.Empty;
            foreach (var taxon in taxa)
                taxonTitles.Add(taxon.Format("{GENUS} {taxon}"));
            return new Error(string.Format("This genus is used in the following taxa: {0}{1}",
                Environment.NewLine,
                string.Join(Environment.NewLine, taxonTitles)));
        }
    }
}
