using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Entities;
using Mediation;
using Servers;
using static Bizmonger.Patterns.MessageBus;

namespace ManageServiceMaterials
{
    public partial class ViewModel
    {
        protected override void MakePromises()
        {
            Subscribe(Messages.REQUEST_SELECTED_SERVICE_RESPONSE, OnSelectedService);
            Subscribe(Messages.REQUEST_SELECTED_MATERIAL_RESPONSE, OnSelectedMaterial);
            Subscribe(Messages.REQUEST_MATERIALS_RESPONSE, OnMaterialsLoaded);
            Subscribe(Messages.MATERIAL_ADDED, OnMaterialAdded);
        }

        protected override void BreakPromises()
        {
            Unsubscribe(Messages.REQUEST_SELECTED_SERVICE_RESPONSE, OnSelectedService);
            Unsubscribe(Messages.REQUEST_SELECTED_MATERIAL_RESPONSE, OnSelectedMaterial);
            Unsubscribe(Messages.REQUEST_MATERIALS_RESPONSE, OnMaterialsLoaded);
            Unsubscribe(Messages.MATERIAL_ADDED, OnNewMaterial);
        }

        void SendRequests()
        {
            Publish(Messages.REQUEST_SELECTED_SERVICE);
            Publish(Messages.REQUEST_MATERIALS);
            Publish(Messages.REQUEST_SERVICE_MATERIALS_MATERIALS_FROM_SERVICE_ID, Service?.Id);
        }

        void OnSelectedService(object obj)
        {
            Service = obj as Service ?? new Service() { Name = "<service name>" };
            AssignedMaterials = new ObservableCollection<Material>(Service.Materials.OrderBy(m => m.Name));

            var materialIds = Service.ServiceMaterials.Select(sm => sm.MaterialId);

            foreach (var materialId in materialIds)
            {
                AssignedMaterials.Add(materialId.ToMaterial());
            }

            UpdateState();
        }

        void OnSelectedMaterial(object obj)
        {
            SelectedAssignedMaterial = obj as Material;
            UpdateState();
        }

        void OnMaterialAdded(object obj)
        {
            var material = obj as Material;
            Materials.Add(material);
            Materials = new ObservableCollection<Material>(Materials.OrderBy(m => m.Name));

            IsDirty = true;
        }

        void OnAddToSelection(object obj)
        {
            if (SelectedMaterialFromCache == null) return;

            ManageAssignedMaterials();
            ManageNonassignedMaterials();

            Materials = new ObservableCollection<Material>(Materials.OrderBy(m => m.Name));

            SelectedMaterialFromCache = null;

            IsDirty = true;

            UpdateState();
        }

        void ManageNonassignedMaterials()
        {
            var alreadyExists = Service.Materials.Contains(SelectedMaterialFromCache);

            if (!alreadyExists)
            {
                Service.Materials.Add(SelectedMaterialFromCache);
            }
        }

        void ManageAssignedMaterials()
        {
            var assignedMaterialAlreadyExists = AssignedMaterials.Contains(SelectedMaterialFromCache);

            if (!assignedMaterialAlreadyExists)
            {
                AssignedMaterials.Add(SelectedMaterialFromCache);
                Materials.Remove(SelectedMaterialFromCache);
            }

            AssignedMaterials = new ObservableCollection<Material>(AssignedMaterials.OrderByDescending(m => m.MarkupPrice));
            Materials = new ObservableCollection<Material>(Materials.OrderBy(m => m.Name));
        }

        void OnNewMaterial(object obj) => Publish(Messages.REQUEST_VIEW_NEW_MATERIAL);

        void OnEdit(object obj) => Publish(Messages.REQUEST_VIEW_EDIT_MATERIAL, SelectedAssignedMaterial);

        void OnMaterialsLoaded(object obj)
        {
            Materials = new ObservableCollection<Material>((obj as IEnumerable<Material>).OrderBy(m => m.Name));
            updateUnassignedMaterials();
        }

        void updateUnassignedMaterials()
        {
            foreach (var material in AssignedMaterials)
            {
                var materialToRemove = Materials.SingleOrDefault(m => m.Id == material.Id);
                var materialFound = materialToRemove != null;

                if (materialFound) Materials.Remove(materialToRemove);
            }

            Materials = new ObservableCollection<Material>(Materials.OrderBy(m => m.Name));
        }

        void OnRemove(object obj)
        {
            AssignedMaterials.Remove(SelectedAssignedMaterial);
            AssignedMaterials = new ObservableCollection<Material>(AssignedMaterials.OrderByDescending(m => m.MarkupPrice));

            Materials.Add(SelectedAssignedMaterial);
            Materials = new ObservableCollection<Material>(Materials.OrderBy(m => m.Name));

            SelectedMaterialFromCache = SelectedAssignedMaterial;

            var materialToRemove = Service.Materials.SingleOrDefault(m => SelectedAssignedMaterial == m);
            Service.Materials.Remove(materialToRemove);

            SelectedAssignedMaterial = AssignedMaterials.FirstOrDefault();

            IsDirty = true;

            UpdateState();
        }

        void OnSaveMaterials(object obj)
        {
            foreach (var material in AssignedMaterials)
            {
                var serviceMaterial = new ServiceMaterial()
                {
                    Id = Guid.NewGuid().ToString(),
                    MaterialId = material.Id,
                    ServiceId = Service.Id ?? Guid.NewGuid().ToString(),
                    Quantity = material.Quantity,
                    UserId = Service.UserId
                };

                Publish(Messages.REQUEST_SAVE_SERVICE_MATERIAL, serviceMaterial);
            }

            Service.Materials = AssignedMaterials;
            Publish(Messages.REQUEST_SAVE_SERVICE, Service);

            IsDirty = false;

            Publish(Messages.REQUEST_SELECTED_SERVICE_RESPONSE, Service);
            Publish(Messages.REQUEST_PREVIOUS_VIEW);
        }

        void UpdateState()
        {
            Add.RaiseCanExecuteChanged();
            Remove.RaiseCanExecuteChanged();
            Continue.RaiseCanExecuteChanged();
            Edit.RaiseCanExecuteChanged();
        }

        internal void Refresh()
        {
            AssignedMaterials = new ObservableCollection<Material>(AssignedMaterials);
        }
    }
}