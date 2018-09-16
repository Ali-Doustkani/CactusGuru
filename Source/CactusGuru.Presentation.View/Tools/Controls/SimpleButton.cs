using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CactusGuru.Presentation.View.Tools.Controls
{
    public class SimpleButton : Button
    {
        static SimpleButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SimpleButton), new FrameworkPropertyMetadata(typeof(SimpleButton)));
            ForegroundProperty.OverrideMetadata(typeof(SimpleButton), new FrameworkPropertyMetadata(OnForegroundChanged));
        }

        public readonly static DependencyProperty OnOverBackgroundProperty = DependencyProperty.Register("OnOverBackground", typeof(Brush), typeof(SimpleButton), new PropertyMetadata(OnOverBackgroundChanged));
        public readonly static DependencyProperty OnOverForegroundProperty = DependencyProperty.Register("OnOverForeground", typeof(Brush), typeof(SimpleButton));
        public readonly static DependencyProperty OnPressedBackgroundProperty = DependencyProperty.Register("OnPressedBackground", typeof(Brush), typeof(SimpleButton));
        public readonly static DependencyProperty OnPressedForegroundProperty = DependencyProperty.Register("OnPressedForeground", typeof(Brush), typeof(SimpleButton));

        public Brush OnOverBackground
        {
            get { return (Brush)GetValue(OnOverBackgroundProperty); }
            set { SetValue(OnOverBackgroundProperty, value); }
        }

        public Brush OnOverForeground
        {
            get { return (Brush)GetValue(OnOverForegroundProperty); }
            set { SetValue(OnOverForegroundProperty, value); }
        }

        public Brush OnPressedBackground
        {
            get { return (Brush)GetValue(OnPressedBackgroundProperty); }
            set { SetValue(OnPressedBackgroundProperty, value); }
        }

        public Brush OnPressedForeground
        {
            get { return (Brush)GetValue(OnPressedForegroundProperty); }
            set { SetValue(OnPressedForegroundProperty, value); }
        }

        private static void OnOverBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d.GetValue(OnPressedBackgroundProperty) == null)
                d.SetValue(OnPressedBackgroundProperty, e.NewValue);
        }

        private static void OnForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d.GetValue(OnOverForegroundProperty) == null)
                d.SetValue(OnOverForegroundProperty, e.NewValue);

            if (d.GetValue(OnPressedForegroundProperty) == null)
                d.SetValue(OnPressedForegroundProperty, e.NewValue);
        }
    }
}