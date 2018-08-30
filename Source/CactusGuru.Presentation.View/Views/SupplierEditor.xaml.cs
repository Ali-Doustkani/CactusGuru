using CactusGuru.Presentation.View.Utils;

namespace CactusGuru.Presentation.View.Views
{
    public partial class SupplierEditor
    {
        public SupplierEditor()
        {
            InitializeComponent();
            _tabController = new TabIndexController();
            _tabController.AddControl(txtTitle);
            _tabController.AddControl(txtAcronym);
            _tabController.AddControl(txtWebSite);
        }

        private readonly TabIndexController _tabController;
    }
}
