using Bizmonger.Patterns;
using Entities;
using Mediation;

namespace EditServiceMaterial
{
    public partial class ViewModel
    {
        readonly MessageBus _messagebus = MessageBus.Instance;
        Material _materialToUpdate = null;

        protected override void MakePromises()
        {
            _messagebus.Subscribe(Messages.REQUEST_SELECTED_MATERIAL_RESPONSE, obj => _materialToUpdate = obj as Material);
            _messagebus.Subscribe(Messages.REQUEST_SAVE_MATERIAL_RESPONSE, obj => IsUpdated = (bool)obj);
        }

        protected override void BreakPromises()
        {
            _messagebus.Unsubscribe(Messages.REQUEST_SELECTED_MATERIAL_RESPONSE, obj => _materialToUpdate = obj as Material);
            _messagebus.Unsubscribe(Messages.REQUEST_SAVE_MATERIAL_RESPONSE, obj => IsUpdated = (bool)obj);
        }

        void SendRequests() => _messagebus.Publish(Messages.REQUEST_SELECTED_MATERIAL);

        void OnCancel(object obj)
        {
            Name = _materialToUpdate.Name;
            Description = _materialToUpdate.Description;
            BaseCost = _materialToUpdate.BaseCost;
            MarkupPrice = _materialToUpdate.MarkupPrice;
            UnitType = _materialToUpdate.UnitType;
            Quantity = _materialToUpdate.Quantity;

            _messagebus.Publish(Messages.REQUEST_PREVIOUS_VIEW);

            BreakPromises();
        }
    }
}