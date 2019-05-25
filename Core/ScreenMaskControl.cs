using System.Windows;
using System.Windows.Controls;

namespace Core
{
    public class ScreenMaskControl : ContentControl
    {
        public static DependencyProperty IsBusyProperty = DependencyProperty.Register(
            "IsBusy", typeof(bool), typeof(ScreenMaskControl), new PropertyMetadata(false));

        public bool IsBusy
        {
            get
            {
                return (bool)GetValue(IsBusyProperty);
            }
            set
            {
                SetValue(IsBusyProperty, value);
            }
        }
    }
}