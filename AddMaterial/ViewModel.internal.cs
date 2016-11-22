using Entities;
using Mediation;
using Mediation.Validation;
using Messaging.Databases;
using static Bizmonger.Patterns.MessageBus;

namespace AddMaterial
{
    public partial class ViewModel
    {
        PromiseProfileDB _promiseProfileDB = new PromiseProfileDB();

        protected override void MakePromises()
        {
            _promiseProfileDB.Execute();

            Subscribe(Messages.REQUEST_SAVE_MATERIAL_RESPONSE, OnSaveResponse);
        }


        protected override void BreakPromises()
        {
            _promiseProfileDB.Revert();

            Unsubscribe(Messages.REQUEST_SAVE_MATERIAL_RESPONSE, OnSaveResponse);
        }

        void OnSaveResponse(object obj)
        {
            IsSaved = (bool)obj;
            Publish(Messages.REQUEST_PREVIOUS_VIEW);
        }

        bool CanSave(object obj)
        {
            decimal basecost = 0.00m;
            var validBaseCost = decimal.TryParse(BaseCost, out basecost);

            decimal markupprice = 0.00m;
            var validMarkupCost = decimal.TryParse(MarkupPrice, out markupprice);

            decimal quantity = 0.00m;
            var validQuantity = decimal.TryParse(MarkupPrice, out quantity);

            return validBaseCost &&
                    validMarkupCost &&
                    validQuantity &&

                    new MaterialValidator().Validate(new Material()
                    {
                        Name = Name,
                        BaseCost = basecost,
                        UnitType = UnitType,
                        Description = Description,
                        MarkupPrice = markupprice,
                        Quantity = quantity,
                    });
        }

        void OnSave(object obj)
        {
            var material = new Material()
            {
                Name = Name,
                Description = Description,
                BaseCost = decimal.Parse(BaseCost),
                MarkupPrice = decimal.Parse(MarkupPrice),
                UnitType = UnitType,
                Quantity = decimal.Parse(Quantity)
            };

            Publish(Messages.REQUEST_SAVE_MATERIAL, material);
            Publish(Messages.REQUEST_VIEW_NEW_MATERIAL_FORM_COMPLETED, material);

            BreakPromises();
        }
    }
}