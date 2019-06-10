using Core;
using CurrencyConverter.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CurrencyConverter.ViewModel
{

    public class CurrencyConvrterViewModel : ViewModelBase, ICurrencyConvrterViewModel
    {
        ObservableCollection<ICountry> countries = null;
        public ObservableCollection<ICountry> Countries
        {
            get { return countries; }
            set
            {
                countries = value;
                NotifyPropertyChanged();
            }
        }

        ICountry fromCountry = null;
        public ICountry FromCountry
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



        ICountry toCountry = null;
        public ICountry ToCountry
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

        public CurrencyConvrterViewModel(ICurrencyConverterService currencyConverterService)
        {
            this.currencyConverterService = currencyConverterService;
            ConvertCommand = new RelayCommand(CanConvert, Convert);

            LoadCountries();
        }

        private async void LoadCountries()
        {
            try
            {
                IsBusy = true;
                Task<ObservableCollection<ICountry>> countriesTask =
                Task.Factory.StartNew(() => currencyConverterService.GetCountries());
                await countriesTask;
                Countries = countriesTask.Result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private bool CanConvert(object arg)
        {
            return true;
        }

        private async void Convert(object arg)
        {
            try
            {
                IsBusy = true;
                Task<decimal> exchangeRateTask = Task.Factory.StartNew(() => currencyConverterService.GetEXchangeRate(FromCountry, ToCountry));
                await exchangeRateTask;
                if (exchangeRateTask.Result > 0)
                {
                    ToAmount = FromAmount * exchangeRateTask.Result;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }



        protected override void ValidateProperty(string propertyName)
        {
            List<string> listErrors = null;
            if (propErrors.TryGetValue(propertyName, out listErrors) == false)
                listErrors = new List<string>();
            else
                listErrors.Clear();

            switch (propertyName)
            {
                case "FromAmount":
                    if (FromAmount < 0)
                    {
                        listErrors.Add("Amount amount can not be less than 0");
                    }
                    if (FromAmount == 0)
                    {
                        listErrors.Add("Amount should be greater than 0");
                    }
                    break;
                default: break;
            }

            if (listErrors.Count > 0)
            {
                NotifyErrorsChanged(propertyName);
            }
        }
    }
}