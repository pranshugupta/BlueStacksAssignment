using CurrencyConverter.BusinessLayer;
using CurrencyConverter.Interfaces;
using CurrencyConverter.Model;
using Microsoft.Practices.Unity;
using Moq;
using NUnit.Framework;
using RestSharp;
using System.Collections.ObjectModel;

namespace CurrencyConverterTest
{
    /// <summary>
    /// Summary description for CurrencyConverterServiceTest
    /// </summary>
    [TestFixture]
    public class CurrencyConverterServiceTest
    {

        [Test]
        public void Test_Sould_be_able_to_get_CountryList()
        {
            //Arrange
            const string countries = "{\"results\":{\"AF\":{\"alpha3\":\"AFG\",\"currencyId\":\"AFN\",\"currencyName\":\"Afghan afghani\",\"currencySymbol\":\"؋\",\"id\":\"AF\",\"name\":\"Afghanistan\"},\"AI\":{\"alpha3\":\"AIA\",\"currencyId\":\"XCD\",\"currencyName\":\"East Caribbean dollar\",\"currencySymbol\":\"$\",\"id\":\"AI\",\"name\":\"Anguilla\"}}}";
            var mockUnityContainer = new Mock<IUnityContainer>();
            var mockRestClient = new Mock<IRestClient>();
            var mockIRestResponse = new Mock<IRestResponse>();
            mockIRestResponse.SetupProperty(x => x.Content, countries);
            mockRestClient.Setup(x => x.Execute(It.IsAny<IRestRequest>())).Returns(() => mockIRestResponse.Object);

            var currencyConverterService = new CurrencyConverterService(mockUnityContainer.Object, mockRestClient.Object);

            //Act
            ObservableCollection<ICountry> countriesList = currencyConverterService.GetCountries();

            //Assert
            Assert.AreEqual(countriesList.Count, 2);
        }

        [Test]
        public void Test_Sould_be_able_to_get_Exchange_Rate()
        {
            //Arrange
            ICountry fromCountry = new Country() { CurrencyId = "USD" };
            ICountry toCountry = new Country() { CurrencyId = "INR" };
            string conversionRate = "{\"USD_INR\":{\"val\":29}}";
            var mockUnityContainer = new Mock<IUnityContainer>();
            var mockRestClient = new Mock<IRestClient>();
            var mockIRestResponse = new Mock<IRestResponse>();
            mockIRestResponse.SetupProperty(x => x.Content, conversionRate);
            mockRestClient.Setup(x => x.Execute(It.IsAny<IRestRequest>())).Returns(() => mockIRestResponse.Object);

            var currencyConverterService = new CurrencyConverterService(mockUnityContainer.Object, mockRestClient.Object);

            //Act
            decimal exchangeRate = currencyConverterService.GetEXchangeRate(fromCountry, toCountry);

            //Assert
            Assert.AreEqual(exchangeRate, 29);
        }
    }
}
