using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Entities;
using Entities.Utilities;
using Mediation;
using Transaction;
using static Bizmonger.Patterns.MessageBus;

namespace CalculateTransaction
{
    public partial class ViewModel
    {
        const string STATEMENT_TYPE_QUOTE = "Quote";

        readonly Registry _registry = new Registry();
        List<Material> _originalMaterials = new List<Material>();
        Dictionary<Service, Entry> _entryDictionary = new Dictionary<Service, Entry>();

        protected override void MakePromises()
        {
            Subscribe(Messages.ENTRY_ADDED, OnEntryAdded);
            Subscribe(Messages.ENTRY_REMOVED, OnEntryRemoved);
            Subscribe(Messages.SERVICE_MODIFIED, OnEntryModified);
        }

        protected override void BreakPromises()
        {
            Unsubscribe(Messages.ENTRY_ADDED, OnEntryAdded);
            Unsubscribe(Messages.ENTRY_REMOVED, OnEntryRemoved);
            Unsubscribe(Messages.SERVICE_MODIFIED, OnEntryModified);
        }

        void OnEntryModified(object obj)
        {
            var service = obj as Service;
            var result = Services.SingleOrDefault(s => s.Id == service.Id);
            bool found = result != null;

            if (found)
            {
                bool canRemove = _entryDictionary.ContainsKey(result);
                Debug.Assert(canRemove);

                if (canRemove)
                {
                    RemoveEntry(result);
                    AddEntry(service);
                }
            }

            UpdateSummary();
            OnPropertyChanged("Services");
        }

        void OnEntryRemoved(object obj)
        {
            var service = obj as Service;
            bool exists = Services.Any(s => s.Id == service.Id);

            if (exists)
            {
                OnRemoveService(service);
            }

            UpdateSummary();
            OnPropertyChanged("Services");
        }

        void OnEntryAdded(object obj)
        {
            var service = obj as Service;

            if (_entryDictionary.ContainsKey(service)) return;

            foreach (var item in Services) if (item.Id == service.Id) return;

            _originalMaterials.Clear();

            ProcessEntry(service);
            OnPropertyChanged("Services");
        }

        void AddEntry(Service service)
        {
            var entry = new Entry()
            {
                Id = new Guid(service.Id),
                Name = service.Name,
                Quantity = 1,
                CurrentMarkupPrice = Math.Round(service.TotalCost(), 2),
                TaxPercentage = Math.Round(service.TaxPercentage, 3)
            };

            Entries.Add(entry);
            _registry.Add(new KeyValuePair<Guid, Entry>(entry.Id, entry));
            _entryDictionary.Add(service, entry);

            bool collectionsInSync = Entries.Count == _registry.Count &&
                                     Entries.Count == _entryDictionary.Count;
            Debug.Assert(collectionsInSync);
        }

        void RemoveEntry(Service service)
        {
            Entry entry = _entryDictionary[service];
            Entries.Remove(entry);

            _registry.Remove(_registry.First(ri => ri.Key == entry.Id));
            _entryDictionary.Remove(service);
        }

        void OnAddService(object obj)
        {
            var service = obj as Service;
            ProcessEntry(service);
        }

        void ProcessEntry(Service service)
        {
            Services.Add(service);

            AddMaterials(service);

            var entry = new Entry()
            {
                Id = new Guid(service.Id),
                Name = service.Name,
                Quantity = 1,
                CurrentMarkupPrice = Math.Round(service.TotalCost(), 2),
                TaxPercentage = Math.Round(service.TaxPercentage, 3)
            };

            Entries.Add(entry);
            _registry.Add(new KeyValuePair<Guid, Entry>(entry.Id, entry));
            _entryDictionary.Add(service, entry);

            SelectedEntry = entry;
            UpdateSummary();
        }

        void OnRemoveService(object obj)
        {
            var service = obj as Service;
            Services.Remove(service);

            _originalMaterials.Clear();

            RemoveMaterials(service);
            decimal materialsCost = DecreaseMaterialsCost();

            RemoveEntry(service);
        }

        decimal IncreaseMaterialsCost()
        {
            decimal materialsCost = 0;
            foreach (var material in _originalMaterials)
            {
                materialsCost += material.BaseCost * material.Quantity;
            }
            return materialsCost;
        }

        decimal DecreaseMaterialsCost()
        {
            decimal materialsCost = 0;
            foreach (var material in _originalMaterials)
            {
                materialsCost -= material.BaseCost * material.Quantity;
            }
            return materialsCost;
        }

        void AddMaterials(Service service)
        {
            if (service.Materials != null)
            {
                foreach (var material in service.Materials)
                {
                    _originalMaterials.Add(material);
                }
            }
        }

        void RemoveMaterials(Service service)
        {
            if (service.Materials != null)
            {
                foreach (var material in service.Materials)
                {
                    _originalMaterials.Remove(material);
                }
            }
        }

        void UpdateSummary()
        {
            if (_registry.Count == 0 && Entries.Count == 0)
            {
                _originalMaterials.Clear();
            }

            decimal materialsCost = GetMaterialCost();
            decimal subtotal = _registry.Subtotal();

            decimal laborCost = 0;

            foreach (var service in _entryDictionary.Keys)
            {
                laborCost += service.LaborCost;
            }

            MaterialsCost = Math.Round(materialsCost, 2);
            LaborCost = Math.Round(laborCost, 2);
            Subtotal = subtotal;
            Tax = Math.Round(_registry.Tax(), 2);
            Total = Math.Round(_registry.Total(), 2);
        }

        decimal GetMaterialCost()
        {
            var materials = new List<Material>();
            Services.Clear();

            foreach (var service in _entryDictionary.Keys)
            {
                Services.Add(service);

                if (service.Materials == null)
                {
                    service.Materials = new ObservableCollection<Material>();
                }

                foreach (var material in service.Materials)
                {
                    materials.Add(material);
                }
            }

            return materials.Cost();
        }

        void RefreshEntries()
        {
            if (Entries.Count == 0 && _registry.Count > 0)
            {
                foreach (var item in _registry)
                {
                    Entries.Add(item.Value);
                }
            }
        }
    }
}