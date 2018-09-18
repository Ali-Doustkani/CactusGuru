using System;
using System.Windows;

namespace CactusGuru.Presentation.View
{
    public static class ResourceLocator
    {
        private static ResourceDictionary _general;
        public static ResourceDictionary General
        {
            get
            {
                if (_general == null)
                {
                    var uri = new Uri("/CactusGuru.Presentation.View;component/Themes/Generic.xaml", UriKind.Relative);
                    _general = (ResourceDictionary)Application.LoadComponent(uri);
                }
                return _general;
            }
        }

        private static ResourceDictionary _dataEntries;
        public static ResourceDictionary DataEntires
        {
            get
            {
                if(_dataEntries==null)
                {
                    var uri = new Uri("/CactusGuru.Presentation.View;component/Resources/DataEntries.xaml", UriKind.Relative);
                    _dataEntries = (ResourceDictionary)Application.LoadComponent(uri);
                }
                return _dataEntries;
            }
        }

        private static ResourceDictionary _lists;
        public static ResourceDictionary Lists
        {
            get
            {
                if (_lists == null)
                {
                    var uri = new Uri("/CactusGuru.Presentation.View;component/Resources/Lists.xaml", UriKind.Relative);
                    _lists = (ResourceDictionary)Application.LoadComponent(uri);
                }
                return _lists;
            }
        }
    }
}
