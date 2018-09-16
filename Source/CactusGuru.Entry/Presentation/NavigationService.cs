using CactusGuru.Entry.CompositionRoot;
using CactusGuru.Presentation.View.Views;
using CactusGuru.Presentation.View.Views.DataEntries;
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
        public void GotoHome()
        {
            SlideTo(ObjectFactory.Instance.GetInstance<FirstPage>());
        }

        public void GotoGenera()
        {
            OpenUserControl(ObjectFactory.Instance.GetInstance<GenusEditor>());
        }

        public void GotoTaxa()
        {
            OpenUserControl(ObjectFactory.Instance.GetInstance<TaxonEditor>());
        }

        public void GotoSuppliers()
        {
            OpenUserControl(ObjectFactory.Instance.GetInstance<SupplierEditor>());
        }

        public void GotoCollectors()
        {
            OpenUserControl(ObjectFactory.Instance.GetInstance<CollectorEditor>());
        }

        public void GotoCollectionItemImageGallary(Guid collectionItem)
        {
            var view = ObjectFactory.Instance.GetInstance<ImageGallary>();
            ((ImageGallaryEditorViewModel)view.DataContext).Load(collectionItem);
            ShowDialog(view);
        }

        public void GotoCollectionItemInserter()
        {
            OpenUserControl(ObjectFactory.Instance.GetInstance<CollectionItemEditor>(), 450, 420);
        }

        public void GotoCollectionItemUpdater(Guid collectionItem)
        {
            var editor = ObjectFactory.Instance.GetInstance<CollectionItemEditor>();
            ((CollectionItemEditorViewModel)editor.DataContext).PrepareForEdit(collectionItem);
            OpenUserControl(editor, 450, 420);
        }

        public void GotoCollectionItemList()
        {
            SlideTo(ObjectFactory.Instance.GetInstance<CollectionItemList>());
        }

        public void GotoImageList()
        {
            SlideTo(ObjectFactory.Instance.GetInstance<ImageList>());
        }

        public void GotoLabelPrint()
        {
            SlideTo(ObjectFactory.Instance.GetInstance<LabelPrint>());
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

        private void OpenUserControl(System.Windows.Controls.Grid view, double width = 420, double height = 480)
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

        private void SlideTo(object uc)
        {
            var frame = (System.Windows.Controls.Frame)typeof(Main).GetField("navFrame", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(System.Windows.Application.Current.MainWindow);
            if (frame.Content != null && uc.GetType() == frame.Content.GetType()) return;
            frame.Navigate(uc);
        }
    }
}
