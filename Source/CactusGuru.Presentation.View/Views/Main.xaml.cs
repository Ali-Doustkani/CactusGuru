using CactusGuru.Presentation.ViewModel.Framework;
using System.Windows.Controls;

namespace CactusGuru.Presentation.View.Views
{
    public partial class Main
    {
        static Main()
        {
            MyDXMessageBoxLocalizer.Active = new MyDXMessageBoxLocalizer();
        }

        public Main()
        {
            InitializeComponent(); 
        }

        private void navFrame_ContentRendered(object sender, System.EventArgs e)
        {
            var vm = ((UserControl)navFrame.Content).DataContext as INavigationViewModel;
            if (vm == null) return;
            vm.Load();
        }
    }
}
