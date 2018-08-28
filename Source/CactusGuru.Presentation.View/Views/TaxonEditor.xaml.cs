using CactusGuru.Presentation.View.Utils;
using System;
using System.Windows.Input;

namespace CactusGuru.Presentation.View.Views
{
    public partial class TaxonEditor : IUserControlView
    {
        public TaxonEditor()
        {
            InitializeComponent();
            _indexController = new TabIndexController();
            _indexController.ReachedToLastField += (a, b) => Save(this, EventArgs.Empty);
            _indexController.AddControl(Genera);
            _indexController.AddControl(species);
            _indexController.AddControl(variety);
            _indexController.AddControl(subspecies);
            _indexController.AddControl(forma);
            _indexController.AddControl(cultivar);
        }

        private readonly TabIndexController _indexController;

        public event EventHandler Save = delegate { };

        private void UIElement_OnKeyUp(object sender, KeyEventArgs e)
        {
            FocusUtil.GotoNextItem(listBox, e);
        }
    }
}
