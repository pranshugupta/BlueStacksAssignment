using CurrencyConverter.Interfaces;
using CurrencyConverter.Model;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.ObjectModel;
using System.Xml;

namespace CurrencyConverter.BusinessLayer
{
    public class CurrencyConverterService : ICurrencyConverterService
    {
        IRestClient restClient;
        public CurrencyConverterService(IRestClient restClient)
        {
            this.restClient = restClient;
        }
        public ObservableCollection<ICountry> GetCountries()
        {
            ObservableCollection<ICountry> countries = null;
            IRestResponse response = MakeRequest(Constants.API_URL_COUNTRIES);

            XmlDocument doc = JsonConvert.DeserializeXmlNode(response.Content);
            if (doc != null)
            {
                XmlNodeList countryNodes = doc.SelectNodes("/results/*");
                if (countryNodes != null && countryNodes.Count > 0)
                {
                    countries = new ObservableCollection<ICountry>();
                    ICountry country;
                    foreach (XmlNode countryNode in countryNodes)
                    {
                        country = new Country();

                        foreach (XmlNode detailNode in countryNode.ChildNodes)
                        {
                            switch (detailNode.Name)
                            {
                                case "name":
                                    country.Name = detailNode.InnerText;
                                    break;
                                case "id":
                                    country.ID = detailNode.InnerText;
                                    break;
                                case "alpha3":
                                    country.Alpha3 = detailNode.InnerText;
                                    break;
                                case "currencyName":
                                    country.CurrencyName = detailNode.InnerText;
                                    break;
                                case "currencyId":
                                    country.CurrencyId = detailNode.InnerText;
                                    break;
                                case "currencySymbol":
                                    country.CurrencySymbol = detailNode.InnerText;
                                    break;
                            }
                        }
                        countries.Add(country);
                    }
                }
            }
            return countries;
        }

        public decimal GetEXchangeRate(ICountry from, ICountry to)
        {
            decimal exchangeRate = 0;
            string conversionFromTo = $"{from.CurrencyId}_{to.CurrencyId}";
            IRestResponse response = MakeRequest(string.Format(Constants.API_URL_EXCHANGE_RATE, conversionFromTo));

            XmlDocument doc = JsonConvert.DeserializeXmlNode(response.Content);
            XmlNode exchangeRateNode = doc.SelectSingleNode(string.Format("/{0}/val", conversionFromTo));
            if (exchangeRateNode != null)
            {
                decimal.TryParse(exchangeRateNode.InnerText, out exchangeRate);
            }
            return exchangeRate;
        }

        IRestResponse MakeRequest(string url)
        {
            restClient.BaseUrl = new Uri(url);
            IRestResponse response = restClient.Execute(new RestRequest());
            return response;
        }
    }
}