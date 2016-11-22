using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using Mediation;
using Mediation.Validation;
using static Bizmonger.Patterns.MessageBus;

namespace AddService
{
    public partial class ViewModel
    {
        Service _service = null;

        protected override void MakePromises()
        {
            Subscribe(Messages.REQUEST_SAVE_SERVICE_RESPONSE, OnAddServiceResponse);
            Subscribe(Messages.REQUEST_SELECTED_SERVICE, OnRequestSelectedService);
        }

        protected override void BreakPromises()
        {
            Unsubscribe(Messages.REQUEST_SAVE_SERVICE_RESPONSE, OnAddServiceResponse);
            Subscribe(Messages.REQUEST_SELECTED_SERVICE, OnRequestSelectedService);
        }

        void OnRequestSelectedService(object obj)
        {
            Publish(Messages.REQUEST_SELECTED_SERVICE_RESPONSE, _service);
        }

        void OnAddServiceResponse(object obj) => Saved = (bool)obj;

        void OnViewMaterials(object obj)
        {
            Save();

            IEnumerable<Material> materials = null;
            SubscribeFirstPublication(Messages.REQUEST_MATERIALS_RESPONSE, payload => materials = payload as IEnumerable<Material>);
            Publish(Messages.REQUEST_MATERIALS);

            if (materials != null && materials.Any())
            {
                _service = _service ?? new Service() { Name = Name };
                Publish(Messages.REQUEST_SAVE_SERVICE, _service);
                Publish(Messages.REQUEST_VIEW_SERVICE_MATERIALS, _service);
            }
            else
            {
                SubscribeFirstPublication(Messages.REQUEST_VIEW_NEW_MATERIAL_FORM_COMPLETED, obj2 =>
                    Publish(Messages.REQUEST_VIEW_SERVICE_MATERIALS, _service));

                Publish(Messages.REQUEST_VIEW_NEW_MATERIAL);
            }
        }

        bool OnCanSave(object obj)
        {
            var name = Name;
            var laborCost = LaborCost;
            var taxPercentage = TaxPercentage;

            return ServiceValidator.Validate(name, laborCost, taxPercentage);
        }

        void OnSave(object obj)
        {
            Save();
            Publish(Messages.REQUEST_PREVIOUS_VIEW);
        }

        void Save()
        {
            decimal taxPercentage = 0.00m;
            var taxPercentageExists = decimal.TryParse(TaxPercentage, out taxPercentage);

            decimal laborCost = 0.00m;
            var laborCostExists = decimal.TryParse(LaborCost, out laborCost);

            _service = _service ?? new Service() { Id = Guid.NewGuid().ToString() };
            _service.Name = Name;
            _service.Description = Description;
            _service.LaborCost = laborCost;
            _service.TaxPercentage = taxPercentage;
            _service.Materials = Materials;

            Publish(Messages.REQUEST_SAVE_SERVICE, _service);
        }
    }
}