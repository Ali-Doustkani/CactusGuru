using CactusGuru.Application.ViewProviders;
using CactusGuru.Presentation.ViewModel.Framework;
using CactusGuru.Presentation.ViewModel.NavigationService;

namespace CactusGuru.Presentation.ViewModel.ViewModels.SupplierViewModels
{
    public class SupplierEditorViewModel : SimpleEditorViewModel<SupplierViewModel>
    {
        public SupplierEditorViewModel(IDataEntryViewProvider dataProvider, IDialogService dialogService)
            : base(dataProvider, new SupplierViewModelFactory(), dialogService)
        {        }

        public override string Title => "تامین کنندگان";

        public string SelectedSupplierFullName
        {
            get { return GetStringProperty(nameof(WorkingItem.FullName)); }
            set { WorkingItem.FullName = value; }
        }

        public string SelectedSupplierAcronym
        {
            get { return GetStringProperty(nameof(WorkingItem.Acronym)); }
            set { WorkingItem.Acronym = value; }
        }

        public string SelectedSupplierWebsite
        {
            get { return GetStringProperty(nameof(WorkingItem.Website)); }
            set { WorkingItem.Website = value; }
        }

    }
}