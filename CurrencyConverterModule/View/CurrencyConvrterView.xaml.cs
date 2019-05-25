using CurrencyConverter.ViewModel;
using System.Windows.Controls;

namespace CurrencyConverter.View
{
    public interface ICurrencyConvrterView { }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CurrencyConvrterView : UserControl
    {
        public CurrencyConvrterView(ICurrencyConvrterViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
