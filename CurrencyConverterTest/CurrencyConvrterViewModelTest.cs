using CurrencyConverter.Interfaces;
using CurrencyConverter.Model;
using CurrencyConverter.ViewModel;
using Moq;
using NUnit.Framework;
using System.Collections.ObjectModel;

namespace CurrencyConverterTest
{
    [TestFixture]
    public class CurrencyConvrterViewModelTest
    {
        [Test]
        public void Should_Populate_Country()
        {
            //Arrange
            ObservableCollection<ICountry> countries = new ObservableCollection<ICountry>() {
                new Country(),
                new Country(),
            };

            var mockService = new Mock<ICurrencyConverterService>();
            mockService.Setup(x => x.GetCountries()).Returns(() => countries);

            //Act
            var viewModel = new CurrencyConvrterViewModel(mockService.Object);

            //Assert
            Mock.Verify();
        }

        [Test]
        public void Should_Populate_ExchangeRate()
        {
            //Arrange
            const decimal rate = 55.545670M;

            var mockService = new Mock<ICurrencyConverterService>();
            mockService.Setup(x => x.GetEXchangeRate(It.IsAny<ICountry>(), It.IsAny<ICountry>())).Returns(() => rate);
            var viewModel = new CurrencyConvrterViewModel(mockService.Object);
            //Act
            viewModel.ConvertCommand.Execute(null);
            //Assert
            Mock.Verify();
        }
    }
}
