using Interfaces.Core;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CurrencyConverter.Interfaces
{
    public interface ICurrencyConvrterViewModel : IViewModelBase
    {
        ObservableCollection<ICountry> Countries { get; set; }
        ICountry FromCountry { get; set; }
        decimal FromAmount { get; set; }
        ICountry ToCountry { get; set; }
        decimal ToAmount { get; set; }
        ICommand ConvertCommand { get; set; }
    }
}