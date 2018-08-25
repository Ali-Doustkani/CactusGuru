using System;
using System.Windows;
using System.Windows.Input;

namespace CactusGuru.Presentation.View.UserControls
{
    public partial class MainMenu
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        public event EventHandler MenuItemClicked = delegate { };

        public static DependencyProperty GeneraCommandProperty = DependencyProperty.Register("GeneraCommand", typeof(ICommand), typeof(MainMenu));
        public static DependencyProperty TaxaCommandProperty = DependencyProperty.Register("TaxaCommand", typeof(ICommand), typeof(MainMenu));
        public static DependencyProperty AddCollectionItemCommandProperty = DependencyProperty.Register("AddCollectionItemCommand", typeof(ICommand), typeof(MainMenu));
        public static DependencyProperty SuppliersCommandProperty = DependencyProperty.Register("SuppliersCommand", typeof(ICommand), typeof(MainMenu));
        public static DependencyProperty CollectorsCommandProperty = DependencyProperty.Register("CollectorsCommand", typeof(ICommand), typeof(MainMenu));
        public static DependencyProperty SeedListsCommandProperty = DependencyProperty.Register("SeedListsCommand", typeof(ICommand), typeof(MainMenu));
        public static DependencyProperty LabelPrintCommandProperty = DependencyProperty.Register("LabelPrintCommand", typeof(ICommand), typeof(MainMenu));
        public static DependencyProperty TransactionCommandProperty = DependencyProperty.Register("TransactionCommand", typeof(ICommand), typeof(MainMenu));
        public static DependencyProperty ImageGalleryCommandProperty = DependencyProperty.Register("ImageGalleryCommand", typeof(ICommand), typeof(MainMenu));

        public ICommand GeneraCommand
        {
            get { return (ICommand)GetValue(GeneraCommandProperty); }
            set { SetValue(GeneraCommandProperty, value); }
        }

        public ICommand TaxaCommand
        {
            get { return (ICommand)GetValue(TaxaCommandProperty); }
            set { SetValue(TaxaCommandProperty, value); }
        }

        public ICommand AddCollectionItemCommand
        {
            get { return (ICommand)GetValue(AddCollectionItemCommandProperty); }
            set { SetValue(AddCollectionItemCommandProperty, value); }
        }

        public ICommand SuppliersCommand
        {
            get { return (ICommand)GetValue(SuppliersCommandProperty); }
            set { SetValue(SuppliersCommandProperty, value); }
        }

        public ICommand CollectorsCommand
        {
            get { return (ICommand)GetValue(CollectorsCommandProperty); }
            set { SetValue(CollectorsCommandProperty, value); }
        }

        public ICommand SeedListsCommand
        {
            get { return (ICommand)GetValue(SeedListsCommandProperty); }
            set { SetValue(SeedListsCommandProperty, value); }
        }

        public ICommand LabelPrintCommand
        {
            get { return (ICommand)GetValue(LabelPrintCommandProperty); }
            set { SetValue(LabelPrintCommandProperty, value); }
        }

        public ICommand TransactionCommand
        {
            get { return (ICommand)GetValue(TransactionCommandProperty); }
            set { SetValue(TransactionCommandProperty, value); }
        }

        public ICommand ImageGalleryCommand
        {
            get { return (ICommand) GetValue(ImageGalleryCommandProperty); }
            set { SetValue(ImageGalleryCommandProperty, value);}
        }

        private void ButtonClicked(object sender, RoutedEventArgs e)
        {
            MenuItemClicked(this, e);
        }
    }
}
