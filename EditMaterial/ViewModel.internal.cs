using Entities;
using Entities.Utilities;
using Mediation;
using static Bizmonger.Patterns.MessageBus;

namespace EditMaterial
{
    public partial class ViewModel
    {
        Material _existingMaterial = null;

        protected override void MakePromises()
        {
            Subscribe(Messages.REQUEST_SELECTED_MATERIAL_RESPONSE, OnSelectedMaterial);
            Subscribe(Messages.REQUEST_SAVE_MATERIAL_RESPONSE, OnMaterialSaved);
        }

        protected override void BreakPromises()
        {
            Unsubscribe(Messages.REQUEST_SELECTED_MATERIAL_RESPONSE, OnSelectedMaterial);
            Unsubscribe(Messages.REQUEST_SAVE_MATERIAL_RESPONSE, OnMaterialSaved);
        }

        void SendRequests() => Publish(Messages.REQUEST_SELECTED_MATERIAL);

        void OnSelectedMaterial(object obj)
        {
            MaterialToUpdate = obj as Material;

            SetOriginal();

            _existingMaterial = new Material();
            MaterialToUpdate.Update(_existingMaterial);
        }

        void SetOriginal()
        {
            Name = MaterialToUpdate.Name;
            Description = MaterialToUpdate.Description;
            BaseCost = MaterialToUpdate.BaseCost;
            MarkupPrice = MaterialToUpdate.MarkupPrice;
            Quantity = MaterialToUpdate.Quantity;
            UnitType = MaterialToUpdate.UnitType;
        }

        void OnMaterialSaved(object obj) => IsUpdated = (bool)obj;

        void OnUpdate(object obj)
        {
            MaterialToUpdate.Name = Name;
            MaterialToUpdate.Description = Description;
            MaterialToUpdate.BaseCost = BaseCost;
            MaterialToUpdate.MarkupPrice = MarkupPrice;
            MaterialToUpdate.Quantity = Quantity;
            MaterialToUpdate.UnitType = UnitType;

            Publish(Messages.REQUEST_SAVE_MATERIAL, MaterialToUpdate);
            Publish(Messages.REQUEST_PREVIOUS_VIEW);

            BreakPromises();
        }

        void OnCancel(object obj)
        {
            SetOriginal();

            Publish(Messages.REQUEST_PREVIOUS_VIEW);

            BreakPromises();
        }
    }
}