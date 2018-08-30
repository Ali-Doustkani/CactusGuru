using CactusGuru.Presentation.View.Utils;

namespace CactusGuru.Presentation.View.Views
{
    public partial class TaxonEditor
    {
        public TaxonEditor()
        {
            InitializeComponent();
            _indexController = new TabIndexController();
            _indexController.AddControl(Genera);
            _indexController.AddControl(species);
            _indexController.AddControl(variety);
            _indexController.AddControl(subspecies);
            _indexController.AddControl(forma);
            _indexController.AddControl(cultivar);
        }

        private readonly TabIndexController _indexController;
    }
}
