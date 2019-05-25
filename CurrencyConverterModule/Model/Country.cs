namespace CurrencyConverter.Model
{
    public interface ICountry
    {
        string Name { get; set; }
        string ID { get; set; }
        string Alpha3 { get; set; }
        string CurrencyName { get; set; }
        string CurrencyId { get; set; }
        string CurrencySymbol { get; set; }
    }

    public class Country : ICountry
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string Alpha3 { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyId { get; set; }
        public string CurrencySymbol { get; set; }
    }
}