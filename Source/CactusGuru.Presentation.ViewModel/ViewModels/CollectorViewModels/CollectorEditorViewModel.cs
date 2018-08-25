﻿using CactusGuru.Application.ViewProviders;
using CactusGuru.Presentation.ViewModel.Framework;
using CactusGuru.Presentation.ViewModel.Framework.DataSourceManagement;
using CactusGuru.Presentation.ViewModel.NavigationService;
using CactusGuru.Presentation.ViewModel.Utils;

namespace CactusGuru.Presentation.ViewModel.ViewModels.CollectorViewModels
{
    public class CollectorEditorViewModel : SimpleEditorViewModel<CollectorViewModel>
    {
        public CollectorEditorViewModel(IDataEntryViewProvider dataProvider,
            IDialogService dialogService,
            IWindowController windowController)
            : base(dataProvider, new CollectorViewModelFactory(), dialogService, windowController)
        {
            ItemSource = new FilterDataSource<CollectorViewModel>();
        }

        public override string Title => "کلکتور ها";

        public string SelectedCollectorFullName
        {
            get { return GetStringProperty(nameof(WorkingItem.FullName)); }
            set { WorkingItem.FullName = value; }
        }

        public string SelectedCollectorAcronym
        {
            get { return GetStringProperty(nameof(WorkingItem.Acronym)); }
            set { WorkingItem.Acronym = value; }
        }

        public string SelectedCollectorWebsite
        {
            get { return GetStringProperty(nameof(WorkingItem.Website)); }
            set { WorkingItem.Website = value; }
        }

        public override void NotifyAllPropertiesChanged()
        {
            OnPropertyChanged(nameof(SelectedCollectorFullName));
            OnPropertyChanged(nameof(SelectedCollectorAcronym));
            OnPropertyChanged(nameof(SelectedCollectorWebsite));
        }
    }
}
