using Domain.ValueObjects;
using System.Diagnostics;

namespace Domain.DomainServices
{
    public class PriceCalculationService
    {
        public Price? CalculatePartialPrice(Price unitPrice, Quantity quantity)
        {
            decimal decimalPrice = 0;
            int intQuantity = 0;
            decimal partialPrice = 0;

            try
            {
                decimalPrice = Convert.ToDecimal(unitPrice.Value);
                intQuantity = Int16.Parse(quantity.Value);
            }
            catch (Exception ex)
            {
                throw new Exception($"error parsing {ex}");
            }

            partialPrice = decimalPrice * intQuantity;

            return Price.Create(partialPrice.ToString());
        }

        public string? CalculateTotalPrice(List<string> priceList)
        {
            var sum = priceList.Sum(x => decimal.Parse(x));

            return sum.ToString();
        }
    }
}
