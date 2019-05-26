using CurrencyConverter.BusinessLayer;
using CurrencyConverter.Interfaces;
using CurrencyConverter.Model;
using CurrencyConverter.View;
using CurrencyConverter.ViewModel;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using RestSharp;

namespace CurrencyConverter
{
    public class CurrencyConverterModule : IModule
    {
        private readonly IRegionManager regionManager;
        private readonly IUnityContainer container;

        public CurrencyConverterModule(IUnityContainer container, IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            this.container = container;

        }
        public void Initialize()
        {
            RegisterTypes();

            regionManager.Regions[Constants.CURRENCY_CONVERTER_REGION]
                .Add(container.Resolve<ICurrencyConverterView>(), Constants.CURRENCY_CONVERTER_REGION);
        }

        private void RegisterTypes()
        {
            container.RegisterInstance<IRestRequest>(new RestRequest());
            container.RegisterInstance<IRestClient>(new RestClient());
            container.RegisterType<ICountry, Country>();
            container.RegisterType<ICurrencyConverterService, CurrencyConverterService>();
            container.RegisterType<ICurrencyConvrterViewModel, CurrencyConvrterViewModel>();
            container.RegisterType<ICurrencyConverterView, CurrencyConvrterView>();
        }
    }
}