using CactusGuru.Presentation.View.Utils;

namespace CactusGuru.Presentation.View.Views
{
    public partial class CollectorEditor
    {
        public CollectorEditor()
        {
            InitializeComponent();
            _controller = new TabIndexController();
            _controller.AddControl(txtTitle);
            _controller.AddControl(txtAcronym);
            _controller.AddControl(txtWebsite);
        }

        private readonly TabIndexController _controller;
    }
}
