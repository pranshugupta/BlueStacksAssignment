using CurrencyConverter.Model;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.ObjectModel;
using System.Xml;

namespace CurrencyConverter.BusinessLayer
{
    public interface ICurrencyConverterService
    {
        ObservableCollection<ICountry> GetCountries();
        decimal GetEXchangeRate(ICountry from, ICountry to);
    }
    public class CurrencyConverterService : ICurrencyConverterService
    {
        public ObservableCollection<ICountry> GetCountries()
        {
            ObservableCollection<ICountry> countries = null;
            var client = new RestClient(@"https://free.currconv.com/api/v7/countries?apiKey=do-not-use");
            var response = client.Execute(new RestRequest());

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
                                case "name": country.Name = detailNode.InnerText; break; break;
                                case "id": country.ID = detailNode.InnerText; break;
                                case "alpha3": country.Alpha3 = detailNode.InnerText; break;
                                case "currencyName": country.CurrencyName = detailNode.InnerText; break;
                                case "currencyId": country.CurrencyId = detailNode.InnerText; break;
                                case "currencySymbol": country.CurrencySymbol = detailNode.InnerText; break;
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
            string requestUrl = string.Format("https://free.currconv.com/api/v7/convert?apiKey=do-not-use&q={0}&compact=y", conversionFromTo);
            var client = new RestClient(requestUrl); ;
            var response = client.Execute(new RestRequest());

            XmlDocument doc = JsonConvert.DeserializeXmlNode(response.Content);
            XmlNode exchangeRateNode = doc.SelectSingleNode(string.Format("/{0}/val", conversionFromTo));
            if (exchangeRateNode != null)
            {
                decimal.TryParse(exchangeRateNode.InnerText, out exchangeRate);
            }

            return exchangeRate;
        }
    }
}