using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace TaskList.Wpf.Controls
{
    /// <summary>
    /// Контрол который позволяет задать Placeholder
    /// </summary>
    public class PlaceholderTextBox : TextBox
    {
        public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register("Placeholder",
                                                                                 typeof(string),
                                                                                 typeof(PlaceholderTextBox),
                                                                                 new PropertyMetadata(OnPlaceholderChanged));

        private static void OnPlaceholderChanged(DependencyObject sender, DependencyPropertyChangedEventArgs ea)
        {
            var tbw = sender as PlaceholderTextBox;
            tbw?.ShowPlaceholder();
        }


        private bool _isHasPlaceholder;
        private bool _hasFocus;
        private Binding _textBinding;


        public PlaceholderTextBox()
        {
            Loaded += (s, ea) => ShowPlaceholder();
        }

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            if(!_hasFocus) ShowPlaceholder();
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            _hasFocus = true;
            HidePlaceholder();
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);
            _hasFocus = false;
            ShowPlaceholder();
        }

        private void ShowPlaceholder()
        {
            if (string.IsNullOrEmpty(Text) && !string.IsNullOrEmpty(Placeholder))
            {
                _isHasPlaceholder = true;

                var bindingExpression = GetBindingExpression(TextProperty);
                bindingExpression?.UpdateSource();
                _textBinding = BindingOperations.GetBinding(this, TextProperty);
                BindingOperations.ClearBinding(this, TextProperty);

                Foreground = new SolidColorBrush(Colors.Gray);
                Text = Placeholder;
            }
        }

        private void HidePlaceholder()
        {
            if (_isHasPlaceholder)
            {
                _isHasPlaceholder = false;
                ClearValue(ForegroundProperty);
                Text = "";

                SetBinding(TextProperty, _textBinding ?? new Binding());
            }
        }
    }
}