using CurrencyConverter.Model;
using System.Collections.ObjectModel;

namespace CurrencyConverter.Interfaces
{

    public interface ICurrencyConverterService
    {
        ObservableCollection<ICountry> GetCountries();
        decimal GetEXchangeRate(ICountry from, ICountry to);
    }
}