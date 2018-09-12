using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CactusGuru.Presentation.View.Controls
{
    public partial class PopupMainMenu
    {
        public PopupMainMenu()
        {
            InitializeComponent();
            popup.Loaded += PopupMainMenu_Loaded;
        }

        private void PopupMainMenu_Loaded(object sender, RoutedEventArgs e)
        {
            var buttons = new List<Button>();
            Action<DependencyObject> findButtons = null;
            findButtons = delegate (DependencyObject depObj)
             {
                 var contentCtrl = depObj as ContentControl;
                 if (contentCtrl?.Content is DependencyObject)
                     findButtons((DependencyObject)contentCtrl.Content);
                 for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                 {
                     var child = VisualTreeHelper.GetChild(depObj, i);
                     if (child != null && child is Button)
                         buttons.Add((Button)child);
                     findButtons(child);
                 }
             };
            findButtons(buttonGrid);

            foreach (var btn in buttons)
                btn.Click += OnButtonClicked;
        }

        private void OnButtonClicked(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = false;
        }

        public static DependencyProperty MenuItemCommandProperty = DependencyProperty.Register("MenuItemCommand",
            typeof(ICommand),
            typeof(PopupMainMenu));


        public ICommand MenuItemCommand
        {
            get { return (ICommand)GetValue(MenuItemCommandProperty); }
            set { SetValue(MenuItemCommandProperty, value); }
        }
    }
}
