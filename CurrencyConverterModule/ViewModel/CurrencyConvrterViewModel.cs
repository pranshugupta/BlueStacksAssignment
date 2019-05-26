﻿using Core;
using CurrencyConverter.BusinessLayer;
using CurrencyConverter.Interfaces;
using RestSharp;
using System;
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

        public CurrencyConvrterViewModel()
        {
            ConvertCommand = new RelayCommand(CanConvert, Convert);
            currencyConverterService = new CurrencyConverterService(new RestClient());

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
            catch (Exception)
            {
                MessageBox.Show("Looks like api server https://free.currconv.com is down. Try again later");
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
                Task<decimal> exchangeRateTask = Task.Factory.StartNew(() => currencyConverterService.GetEXchangeRate(FromCountry, toCountry));
                await exchangeRateTask;
                if (exchangeRateTask.Result > 0)
                {
                    ToAmount = FromAmount * exchangeRateTask.Result;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Looks like api server https://free.currconv.com is down. Try again later");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}