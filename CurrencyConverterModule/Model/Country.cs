using RestSharp.Deserializers;

namespace CurrencyConverter.Model
{
    public class Country
    {
        [DeserializeAs(Name = "name")]
        public string Name { get; set; }


        [DeserializeAs(Name = "id")]
        public string ID { get; set; }


        [DeserializeAs(Name = "alpha3")]
        public string Alpha3 { get; set; }


        [DeserializeAs(Name = "currencyName")]
        public string CurrencyName { get; set; }


        [DeserializeAs(Name = "currencyId")]
        public string CurrencyId { get; set; }


        [DeserializeAs(Name = "currencySymbol")]
        public string CurrencySymbol { get; set; }
    }
}