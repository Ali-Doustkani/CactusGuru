﻿using System;
using System.Collections.Generic;
using System.Linq;
using CactusGuru.Domain.Greenhouse.Formatting;
using CactusGuru.Domain.Persistance.Repositories;
using CactusGuru.Infrastructure.Persistance;
using CactusGuru.Infrastructure.Qualification;

namespace CactusGuru.Domain.Greenhouse.Qualification.Inquiries
{
    public class GeneraInquiry : InquiryBase<Genus>
    {
        public GeneraInquiry(IUnitOfWork uow, IFormatter<Taxon> taxonFormatter)
        {
            _uow = uow;
            _taxonFormatter = taxonFormatter;
        }

        private readonly IUnitOfWork _uow;
        private readonly IFormatter<Taxon> _taxonFormatter;

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
            var taxa = _uow.CreateRepository<ITaxonRepository>().GetByGeneraId(genusId);
            if (!taxa.Any()) return Error.Empty;
            foreach (var taxon in taxa)
                taxonTitles.Add(_taxonFormatter.Format(taxon));
            return new Error(string.Format("جنس مورد نظر در تعریف تاکسون های ذیل استفاده شده است: {0}{1}",
                Environment.NewLine,
                string.Join(Environment.NewLine, taxonTitles)));
        }
    }
}