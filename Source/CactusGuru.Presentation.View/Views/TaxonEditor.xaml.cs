using System.Windows;
using System.Windows.Input;
using CactusGuru.Presentation.View.Utils;
using CactusGuru.Presentation.ViewModel.Utils;
using System;

namespace CactusGuru.Presentation.View.Views
{
    public partial class TaxonEditor : IWindowController,IUserControlView
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

        private void TaxonEditor_OnLoaded(object sender, RoutedEventArgs e)
        {
            listBox.Focus();
        }

        private void UIElement_OnKeyUp(object sender, KeyEventArgs e)
        {
            FocusUtil.GotoNextItem(listBox, e);
        }

        public void FocusFirstControl()
        {
            Genera.Focus();
        }

        public void FocusOnSearch()
        {
            txtSearch.Focus();
        }
    }
}
