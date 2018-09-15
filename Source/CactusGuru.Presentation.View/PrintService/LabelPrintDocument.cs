using CactusGuru.Presentation.ViewModel.PrintService;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;

namespace CactusGuru.Presentation.View.PrintService
{
    public class LabelPrintDocument
    {
        private const string TEMPLATE = "CactusGuru.Presentation.View.PrintService.LabelTemplate.xaml";
        private const int PAGE_CAPACITY = 36;

        public LabelPrintDocument(IEnumerable<LabelPrintItemDto> labels)
        {
            _labels = labels;
            A4 = new Size(794, 1096);
        }
      
        private readonly IEnumerable<LabelPrintItemDto> _labels;
        private readonly Size A4;

        public DocumentPaginator GenerateDocument()
        {
            var document = new FixedDocument();
            document.DocumentPaginator.PageSize = A4;

            var taken = 0;
            while (taken != _labels.Count())
            {
                var source = _labels.Skip(taken).Take(PAGE_CAPACITY);
                taken += source.Count();
                document.Pages.Add(PageFor(source));
            }
            return document.DocumentPaginator;
        }

        private PageContent PageFor(IEnumerable<LabelPrintItemDto> pageSource)
        {
            var page = new FixedPage();
            page.Width = A4.Width;
            page.Height = A4.Height;
            var content = ListBox();
            content.ItemsSource = pageSource;
            page.Children.Add(content);
            var pageContent = new PageContent();
            ((IAddChild)pageContent).AddChild(page);
            return pageContent;
        }

        private ListBox ListBox()
        {
            var listbox = new ListBox();
            listbox.ItemsPanel = new ItemsPanelTemplate(new FrameworkElementFactory(typeof(WrapPanel)));
            listbox.BorderThickness = new Thickness(0);
            listbox.SetValue(ScrollViewer.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Disabled);
            listbox.SetValue(ScrollViewer.VerticalScrollBarVisibilityProperty, ScrollBarVisibility.Disabled);
            var itemTemplate = new ControlTemplate(typeof(ListBoxItem));
            itemTemplate.VisualTree = new FrameworkElementFactory(typeof(ContentPresenter));
            listbox.ItemContainerStyle = new Style { TargetType = typeof(ListBoxItem) };
            listbox.ItemContainerStyle.Setters.Add(new Setter { Property = Control.TemplateProperty, Value = itemTemplate });
            using (var stream = GetType().Assembly.GetManifestResourceStream(TEMPLATE))
            {
                var reader = new XamlReader();
                listbox.ItemTemplate = (DataTemplate)reader.LoadAsync(stream);
            }
            listbox.Margin = new Thickness(20, 10, 10, 20);
            listbox.Width = 800;
            return listbox;
        }
    }
}
