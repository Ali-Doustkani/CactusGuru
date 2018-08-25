using CactusGuru.Infrastructure;
using CactusGuru.Presentation.View.Views;
using CactusGuru.Presentation.ViewModel.NavigationService;
using CactusGuru.Presentation.ViewModel.ViewModels.CollectionItemViewModels;
using CactusGuru.Presentation.ViewModel.ViewModels.ImageGallery;
using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;

namespace CactusGuru.Entry.Presentation
{
    public class NavigationService : INavigationService
    {
        public NavigationService(IResolver resolver)
        {
            _resolver = resolver;
        }

        private readonly IResolver _resolver;

        public void GotoHome()
        {
            SlideTo(_resolver.Resolve<FirstPage>());
        }

        public void GotoGenera()
        {
            OpenUserControl(_resolver.Resolve<GenusEditor>());
        }

        public void GotoTaxa()
        {
            OpenUserControl(_resolver.Resolve<TaxonEditor>());
        }

        public void GotoSuppliers()
        {
            OpenUserControl(_resolver.Resolve<SupplierEditor>());
        }

        public void GotoCollectors()
        {
            OpenUserControl(_resolver.Resolve<CollectorEditor>());
        }

        public void GotoCollectionItemImageGallary(Guid collectionItem)
        {
            var view = _resolver.Resolve<ImageGallary>();
            ((ImageGallaryEditorViewModel)view.DataContext).Load(collectionItem);
            ShowDialog(view);
        }

        public void GotoCollectionItemInserter()
        {
            OpenUserControl(_resolver.Resolve<CollectionItemEditor>("forInsert"), 450, 420);
        }

        public void GotoCollectionItemUpdater(Guid collectionItem)
        {
            var editor = _resolver.Resolve<CollectionItemEditor>("forUpdate");
            ((CollectionItemEditorViewModel)editor.DataContext).PrepareForEdit(collectionItem);
            OpenUserControl(editor, 450, 420);
        }

        public void GotoCollectionItemList()
        {
            SlideTo(_resolver.Resolve<CollectionItemList>());
        }

        public void GotoImageList()
        {
            SlideTo(_resolver.Resolve<ImageList>());
        }

        public void GotoLabelPrint()
        {
            var openedWindow = System.Windows.Application.Current.Windows.OfType<LabelPrint>().SingleOrDefault();
            if (openedWindow == null)
            {
                var window = _resolver.Resolve<LabelPrint>();
                window.Show();
            }
            else
            {
                openedWindow.Activate();
            }
        }

        public void GotoTransaction()
        {
            var view = _resolver.Resolve<TransactionEditor>();
            view.ResizeMode = ResizeMode.CanResize;
            ShowDialog(view);
        }

        public DialogResult<int> GetNumberFromUser()
        {
            var view = new LabelPrintCount();
            view.Owner = System.Windows.Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            var dialogResult = view.ShowDialog();
            if (dialogResult.HasValue)
                return new DialogResult<int>(dialogResult.Value, view.GetCount());
            return new DialogResult<int>(false, view.GetCount());
        }

        public DialogResult<DateTime> GetDateFromUser()
        {
            var view = new ImageGallaryDate();
            view.Owner = System.Windows.Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            var dialogResult = view.ShowDialog();
            if (dialogResult.HasValue)
                return new DialogResult<DateTime>(dialogResult.Value, view.GetDate());
            return new DialogResult<DateTime>(false, view.GetDate());
        }

        public Image SelectImage()
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "JPEG Files|*.jpg";
            dialog.ShowDialog();
            if (!File.Exists(dialog.FileName))
                return null;
            return Image.FromFile(dialog.FileName);
        }

        public void CloseCurrentView()
        {
            var activeWindow = System.Windows.Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            if (activeWindow != null)
                activeWindow.Close();
        }

        private void OpenUserControl(IUserControlView view, double width = 420, double height = 480)
        {
            var window = new BaseEditorWindow();
            window.DataContext = view.DataContext;
            window.SetUserControl(view);
            window.Owner = System.Windows.Application.Current.MainWindow;
            window.ResizeMode = ResizeMode.NoResize;
            window.Width = width;
            window.Height = height;
            window.ShowDialog();
        }

        private void ShowDialog(Window window)
        {
            window.Owner = System.Windows.Application.Current.MainWindow;
            window.ShowDialog();
        }

        private void SlideTo(System.Windows.Controls.UserControl uc)
        {
            var frame = (DevExpress.Xpf.WindowsUI.NavigationFrame)typeof(Main).GetField("navFrame", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(System.Windows.Application.Current.MainWindow);
            if (frame.Content != null && uc.GetType() == frame.Content.GetType()) return;
            frame.Navigate(uc);
        }
    }
}
