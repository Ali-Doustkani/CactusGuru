using CactusGuru.Presentation.ViewModel.Framework;
using System;
using CactusGuru.Application.Common;

namespace CactusGuru.Presentation.ViewModel.ViewModels.CollectionItemViewModels
{
    public class CollectionItemViewModel : WorkingViewModel
    {
        public string Code
        {
            get { return Inner<CollectionItemDto>().Code; }
            set { Inner<CollectionItemDto>().Code = value; }
        }

        public int? Count
        {
            get { return Inner<CollectionItemDto>().Count; }
            set { Inner<CollectionItemDto>().Count = value; }
        }

        public string FieldNumber
        {
            get { return Inner<CollectionItemDto>().FieldNumber; }
            set { Inner<CollectionItemDto>().FieldNumber = value; }
        }

        public string SupplierCode
        {
            get { return Inner<CollectionItemDto>().SupplierCode; }
            set { Inner<CollectionItemDto>().SupplierCode = value; }
        }

        public string Locality
        {
            get { return Inner<CollectionItemDto>().Locality; }
            set { Inner<CollectionItemDto>().Locality = value; }
        }

        public DateTime? IncomeDate
        {
            get { return Inner<CollectionItemDto>().IncomeDate; }
            set { Inner<CollectionItemDto>().IncomeDate = value; }
        }

        public IncomeTypeDto IncomeType
        {
            get { return Inner<CollectionItemDto>().IncomeType; }
            set { Inner<CollectionItemDto>().IncomeType = value; }
        }

        public string Description
        {
            get { return Inner<CollectionItemDto>().Description; }
            set { Inner<CollectionItemDto>().Description = value; }
        }

        public Guid? Taxon
        {
            get { return Inner<CollectionItemDto>().Taxon; }
            set { Inner<CollectionItemDto>().Taxon = value; }
        }

        public Guid? Collector
        {
            get { return Inner<CollectionItemDto>().Collector; }
            set { Inner<CollectionItemDto>().Collector = value; }
        }

        public Guid? Supplier
        {
            get { return Inner<CollectionItemDto>().Supplier; }
            set { Inner<CollectionItemDto>().Supplier = value; }
        }
    }
}
