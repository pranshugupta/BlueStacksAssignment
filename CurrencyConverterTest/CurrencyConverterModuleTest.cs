using CurrencyConverter;
using CurrencyConverter.Interfaces;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using Moq;
using NUnit.Framework;

namespace CurrencyConverterTest
{
    [TestFixture]
    public class CurrencyConverterModuleTest
    {
        [Test]
        public void Verify_Module_Is_Initialize()
        {
            // Arrage
            var mockUnityContainer = new Mock<IUnityContainer>();
            var mockRegionManager = new Mock<IRegionManager>();
            mockRegionManager.Setup(x => x.Regions[It.IsAny<string>()].Add(It.IsAny<ICurrencyConverterView>()));
            var currencyConverterModule = new CurrencyConverterModule(mockUnityContainer.Object, mockRegionManager.Object);

            //Act
            currencyConverterModule.Initialize();
            //Assert
            Mock.VerifyAll();
        }
    }
}
