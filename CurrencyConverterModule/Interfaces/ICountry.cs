namespace CurrencyConverter.Interfaces
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
}