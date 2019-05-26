using CurrencyConverter.Interfaces;
using CurrencyConverter.ViewModel;
using System.Windows.Controls;

namespace CurrencyConverter.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CurrencyConvrterView : UserControl, ICurrencyConverterView
    {
        public CurrencyConvrterView(ICurrencyConvrterViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
