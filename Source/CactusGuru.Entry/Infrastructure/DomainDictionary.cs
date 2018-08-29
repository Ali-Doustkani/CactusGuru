using CactusGuru.Domain.Greenhouse;
using CactusGuru.Infrastructure.Qualification;
using System;

namespace CactusGuru.Entry.Infrastructure
{
    public class DomainDictionary : IDomainDictionary
    {
        public string Translate(string name)
        {
            if (name == nameof(Genus))
                return "جنس";
            if (name == nameof(Taxon))
                return "تاکسون";
            if (name == nameof(Collector))
                return "کالکتور";
            if (name == nameof(Supplier))
                return "تامین کننده";
            if (name == nameof(Taxon.Species))
                return "گونه";
            if (name == nameof(CollectionItem.Code))
                return "کد";
            if (name == nameof(Collector.Acronym))
                return "اختصار";
            if (name == nameof(Collector.FullName))
                return "نام";
            if (name == nameof(Genus.Title))
                return "عنوان";
            throw new InvalidOperationException("Domain Dictionary lacks your finding word.");
        }
    }
}
