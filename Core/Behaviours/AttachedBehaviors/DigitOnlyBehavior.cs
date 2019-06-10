using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Core.Behaviours.AttachedBehaviors
{
    public static class DigitOnlyBehavior
    {
        public static readonly DependencyProperty IsDigitOnlyProperty =
            DependencyProperty.RegisterAttached("IsDigitOnly", typeof(bool), typeof(DigitOnlyBehavior), new PropertyMetadata(false, OnIsNumericOnlyChanged));

        public static bool GetIsDigitOnly(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsDigitOnlyProperty);
        }

        public static void SetIsDigitOnly(DependencyObject obj, bool value)
        {
            obj.SetValue(IsDigitOnlyProperty, value);
        }

        private static void OnIsNumericOnlyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var textBox = (TextBox)sender;
            var isNumericOnly = (bool)(args.NewValue);

            if (isNumericOnly)
            {
                textBox.PreviewTextInput += TextBox_PreviewTextInput;
            }
            else
            {
                textBox.PreviewTextInput -= TextBox_PreviewTextInput;
            }
        }

        private static void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs args)
        {
            args.Handled = args.Text.Any(c => !char.IsDigit(c));
        }
    }
}
