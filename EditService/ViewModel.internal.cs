using Entities;
using Entities.Utilities;
using Mediation;
using Mediation.Validation;
using static Bizmonger.Patterns.MessageBus;

namespace EditService
{
    public partial class ViewModel
    {
        Service _modifiedService = null;

        protected override void MakePromises()
        {
            Subscribe(Messages.REQUEST_SELECTED_SERVICE_RESPONSE, OnSelectedService);
            Subscribe(Messages.REQUEST_SELECTED_SERVICE, OnRequestSelectedService);
            Subscribe(Messages.REQUEST_SAVE_SERVICE_RESPONSE, OnServiceUpdated);
            Subscribe(Messages.MATERIAL_ADDED, OnMaterialAdded);
        }

        protected override void BreakPromises()
        {
            Unsubscribe(Messages.REQUEST_SELECTED_SERVICE_RESPONSE, OnSelectedService);
            Unsubscribe(Messages.REQUEST_SELECTED_SERVICE, OnRequestSelectedService);
            Unsubscribe(Messages.REQUEST_SAVE_SERVICE_RESPONSE, OnServiceUpdated);
            Unsubscribe(Messages.MATERIAL_ADDED, OnMaterialAdded);
        }

        void SendRequests() => Publish(Messages.REQUEST_SELECTED_SERVICE);

        void OnMaterialAdded(object obj)
        {
            var material = obj as Material;

            var alreadyExists = Materials.Contains(material);
            if (alreadyExists) return;

            Materials.Add(material);
        }

        void OnRequestSelectedService(object obj)
        {
            if (ServiceToUpdate != null)
            {
                Publish(Messages.REQUEST_SELECTED_SERVICE_RESPONSE, ServiceToUpdate);
            }
        }

        void OnSelectedService(object obj)
        {
            ServiceToUpdate = obj as Service;

            Name = ServiceToUpdate.Name;
            Description = ServiceToUpdate.Description;
            LaborCost = ServiceToUpdate.LaborCost.ToString();
            TaxPercentage = ServiceToUpdate.TaxPercentage.ToString();
            Materials = ServiceToUpdate.Materials;

            _modifiedService = new Service();
            ServiceToUpdate.Update(_modifiedService);
        }

        void OnServiceUpdated(object obj) => IsUpdated = (bool)obj;

        void OnUpdate(object obj)
        {
            AssignValues();

            Publish(Messages.REQUEST_SAVE_SERVICE, ServiceToUpdate);
            Publish(Messages.REQUEST_PREVIOUS_VIEW);
        }

        void AssignValues()
        {
            ServiceToUpdate.Name = Name;
            ServiceToUpdate.Description = Description;
            ServiceToUpdate.LaborCost = decimal.Parse(LaborCost);
            ServiceToUpdate.TaxPercentage = decimal.Parse(TaxPercentage);
            ServiceToUpdate.Materials = Materials;
        }

        bool OnCanSave(object obj)
        {
            var name = Name;
            var laborCost = LaborCost;
            var taxPercentage = TaxPercentage;

            return ServiceValidator.Validate(name, laborCost, taxPercentage);
        }

        void OnCancel(object obj)
        {
            BreakPromises();
            Publish(Messages.REQUEST_PREVIOUS_VIEW);
        }

        void OnViewMaterials(object obj)
        {
            AssignValues();
            Publish(Messages.REQUEST_VIEW_SERVICE_MATERIALS, ServiceToUpdate);
        }
    }
}