using Core;
using CurrencyConverter.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CurrencyConverter.ViewModel
{
    public class CurrencyConvrterViewModel : ViewModelBase
    {
        ObservableCollection<Country> countries = null;
        public ObservableCollection<Country> Countries
        {
            get { return countries; }
            set
            {
                countries = value;
                NotifyPropertyChanged();
            }
        }


        Country fromCountry = null;
        public Country FromCountry
        {
            get { return fromCountry; }
            set
            {
                fromCountry = value;
                NotifyPropertyChanged();
            }
        }

        decimal fromAmount;
        public decimal FromAmount
        {
            get { return fromAmount; }
            set
            {
                fromAmount = value;
                NotifyPropertyChanged();
            }
        }



        Country toCountry = null;
        public Country ToCountry
        {
            get { return toCountry; }
            set
            {
                toCountry = value;
                NotifyPropertyChanged();
            }
        }

        decimal toAmount = 0;
        public decimal ToAmount
        {
            get { return toAmount; }
            set
            {
                toAmount = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand ConvertCommand { get; set; }

        ICurrencyConverterService currencyConverterService;

        public CurrencyConvrterViewModel()
        {
            ConvertCommand = new RelayCommand(CanConvert, Convert);
            currencyConverterService = new CurrencyConverterService();

            LoadCountries();
        }

        private async void LoadCountries()
        {
            Task<ObservableCollection<Country>> countriesTask =
                Task.Factory.StartNew(() => currencyConverterService.GetCountries());
            await countriesTask;
            Countries = countriesTask.Result;
        }

        private bool CanConvert(object arg)
        {
            return true;
        }

        private async void Convert(object arg)
        {
            Task<decimal> exchangeRateTask = Task.Factory.StartNew(() => currencyConverterService.GetEXchangeRate(FromCountry, toCountry));
            await exchangeRateTask;
            if (exchangeRateTask.Result > 0)
            {
                ToAmount = FromAmount * exchangeRateTask.Result;
            }
        }

    }
}