using Core;
using CurrencyConverter.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CurrencyConverter.ViewModel
{
    public class CurrencyConvrterViewModel : ViewModelBase
    {
        ObservableCollection<Country> countries = new ObservableCollection<Country>();
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

        public CurrencyConvrterViewModel()
        {
            ConvertCommand = new RelayCommand(CanConvert, Convert);
        }

        private bool CanConvert(object arg)
        {
            throw new NotImplementedException();
        }

        private void Convert(object arg)
        {
            throw new NotImplementedException();
        }
    }
}