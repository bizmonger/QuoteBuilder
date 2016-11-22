using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Entities;
using Mediation;
using static Bizmonger.Patterns.MessageBus;

namespace ManageMaterials
{
    public partial class ViewModel
    {
        protected override void MakePromises()
        {
            Subscribe(Messages.REQUEST_MATERIALS_RESPONSE, OnMaterialsLoaded);
            Subscribe(Messages.MATERIAL_ADDED, OnMaterialAdded);
        }

        protected override void BreakPromises()
        {
            Unsubscribe(Messages.REQUEST_MATERIALS_RESPONSE, OnMaterialsLoaded);
            Unsubscribe(Messages.MATERIAL_ADDED, OnMaterialAdded);
        }

        void SendRequests() => Publish(Messages.REQUEST_MATERIALS);

        void OnMaterialAdded(object obj)
        {
            var material = obj as Material;
            Materials.Add(material);
            Materials.OrderBy(m => m.Name);
        }

        void OnMaterialsLoaded(object obj) =>
            Materials = new ObservableCollection<Material>((obj as IEnumerable<Material>).OrderBy(m => m.Name));
    }
}