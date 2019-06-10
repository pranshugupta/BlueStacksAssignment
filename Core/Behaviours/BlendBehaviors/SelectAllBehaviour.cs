using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Core// .Behaviours.BlendBehaviors
{
    public class SelectAllBehaviour : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.GotFocus += AssociatedObject_GotFocus;
        }
        protected override void OnDetaching()
        {
            AssociatedObject.GotFocus -= AssociatedObject_GotFocus;
            base.OnDetaching();
        }

        private void AssociatedObject_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            AssociatedObject.SelectAll();
        }
    }
}
