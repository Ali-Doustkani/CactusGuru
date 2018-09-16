using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CactusGuru.Presentation.View.Tools.Controls
{
    public class NullTextBox : TextBox, INotifyPropertyChanged
    {
        public readonly static DependencyProperty NullTextProperty = DependencyProperty.Register("NullText", typeof(string), typeof(NullTextBox));

        static NullTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NullTextBox), new FrameworkPropertyMetadata(typeof(NullTextBox)));
            BorderThicknessProperty.OverrideMetadata(typeof(NullTextBox), new FrameworkPropertyMetadata(new Thickness(1)));
            BorderBrushProperty.OverrideMetadata(typeof(NullTextBox), new FrameworkPropertyMetadata(Brushes.Gray));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string NullText
        {
            get { return (string)GetValue(NullTextProperty); }
            set { SetValue(NullTextProperty, value); }
        }

        public Visibility NullTextVisibility
        {
            get
            {
                if (string.IsNullOrEmpty(Text) && !IsKeyboardFocused)
                    return Visibility.Visible;
                return Visibility.Collapsed;
            }
        }

        protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnGotKeyboardFocus(e);
            VisibilityChanged();
        }

        protected override void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnLostKeyboardFocus(e);
            VisibilityChanged();
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            if (!IsKeyboardFocused)
                VisibilityChanged();
        }

        private void VisibilityChanged() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NullTextVisibility)));
    }
}
