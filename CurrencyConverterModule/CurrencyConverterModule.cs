
using CurrencyConverter.View;
using CurrencyConverter.ViewModel;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace CurrencyConverter
{
    public class CurrencyConverterModule : IModule
    {
        private readonly IRegionManager regionManager;

        public CurrencyConverterModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;

        }
        public void Initialize()
        {
            regionManager.Regions[RegionConstants.CurrencyConverterRegion].Add(new CurrencyConvrterView(new CurrencyConvrterViewModel()), RegionConstants.CurrencyConverterRegion);
        }
    }
}