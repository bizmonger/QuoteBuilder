using TestAPI;
using static TestAPI.Gimme;

namespace AddService.Tests
{
    public partial class _AddService
    {
        static void CreateServiceWithMaterial(ViewModel addServiceviewModel)
        {
            addServiceviewModel.Name = SOME_TEXT;
            addServiceviewModel.LaborCost = SOME_DECIMAL_VALUE.ToString();
            addServiceviewModel.TaxPercentage = SOME_DECIMAL_VALUE.ToString();
            addServiceviewModel.Description = SOME_TEXT;
            addServiceviewModel.Materials.Add(Mocks.MATERIAL_1);
        }
    }
}