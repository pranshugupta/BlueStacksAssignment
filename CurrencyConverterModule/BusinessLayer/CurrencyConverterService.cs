using CurrencyConverter.Interfaces;
using CurrencyConverter.Model;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.ObjectModel;
using System.Xml;

namespace CurrencyConverter.BusinessLayer
{
    public class CurrencyConverterService : ICurrencyConverterService
    {
        private readonly IRestClient restClient;
        private readonly IUnityContainer container;
        public CurrencyConverterService(IUnityContainer container, IRestClient restClient)
        {
            this.restClient = restClient;
            this.container = container;
        }
        public ObservableCollection<ICountry> GetCountries()
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception("Looks like api server https://free.currconv.com is down. Try again later");
            }
        }

        public decimal GetEXchangeRate(ICountry from, ICountry to)
        {
            try
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
            catch (Exception)
            {
                throw new Exception("Looks like api server https://free.currconv.com is down. Try again later");
            }
        }

        IRestResponse MakeRequest(string url)
        {
            restClient.BaseUrl = new Uri(url);
            IRestResponse response = restClient.Execute(container.Resolve<IRestRequest>());
            return response;
        }
    }
}