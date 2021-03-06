﻿using CactusGuru.Application.ViewProviders.CollectionItems;
using CactusGuru.Presentation.ViewModel.Framework;
using System;

namespace CactusGuru.Presentation.ViewModel.ViewModels.CollectionItemListViewModels
{
    public class CollectionItemViewModel : WorkingViewModel
    {
        public CollectionItemViewModel(CollectionItemDto dto)
         : base(dto)
        { }

        public string Code
        {
            get { return Inner<CollectionItemDto>().Code; }
            set { Inner<CollectionItemDto>().Code = value; }
        }

        public string Name
        {
            get { return Inner<CollectionItemDto>().Name; }
            set
            {
                Inner<CollectionItemDto>().Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Info
        {
            get { return Inner<CollectionItemDto>().Info; }
            set { Inner<CollectionItemDto>().Info = value; }
        }

        public Guid TaxonId
        {
            get { return Inner<CollectionItemDto>().TaxonId; }
        }

        public Guid GenusId
        {
            get { return Inner<CollectionItemDto>().GenusId; }
        }
    }
}
