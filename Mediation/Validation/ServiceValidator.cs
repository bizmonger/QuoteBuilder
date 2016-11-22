using Entities;

namespace Mediation.Validation
{
    public class ServiceValidator
    {
        public static bool Validate(string name, string laborCost, string taxPercentage)
        {
            var parsedTaxPercentage = 0.00m;
            var parsedLaborCost = 0.00m;

            var isValidCost = decimal.TryParse(laborCost, out parsedLaborCost);
            if (!isValidCost) return false;

            var isValidTaxPercentage = decimal.TryParse(taxPercentage, out parsedTaxPercentage);
            if (!isValidTaxPercentage) return false;

            var validator = new ServiceValidator();
            return validator.Validate(new Service() { Name = name, LaborCost = parsedLaborCost, TaxPercentage = parsedTaxPercentage });
        }

        public bool Validate(Service service) =>
                !string.IsNullOrWhiteSpace(service.Name) &&
                service.LaborCost >= 0 &&
                service.TaxPercentage >= 0;
    }
}