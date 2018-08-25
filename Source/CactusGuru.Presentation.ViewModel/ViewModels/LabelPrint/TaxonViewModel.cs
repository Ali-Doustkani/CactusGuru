using CactusGuru.Application.ViewProviders.LabelPrinting;
using CactusGuru.Presentation.ViewModel.Framework;

namespace CactusGuru.Presentation.ViewModel.ViewModels.LabelPrint
{
    public class TaxonViewModel : BaseViewModel
    {
        public TaxonViewModel(TaxonDto innerObject)
        {
            _innerObject = innerObject;
        }

        private TaxonDto _innerObject;
        public TaxonDto InnerObject
        {
            get { return _innerObject; }
            set
            {
                _innerObject = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Name
        {
            get { return InnerObject.Name; }
        }
    }
}