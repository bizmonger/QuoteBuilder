using System;
using System.Collections.Generic;
using System.Linq;

namespace Transaction
{
    public partial class Registry
    {
        public void ProcessEntries(List<Entry> entries)
        {
            Clear();

            foreach (var entry in entries)
            {
                var registeredEntries = this.Where(item => item.Key == entry.Id);
                {
                    Register(entry);
                }
            }
        }

        public Entry ProcessEntry(Guid productID, decimal minimumPrice, decimal quantity, decimal price, decimal taxRateForProduct, bool isTaxExempt, List<Entry> _sourceEntries, decimal discountPercentage = 0)
        {
            Entry sourceEntry = Entry.Create(productID, minimumPrice, quantity, price, taxRateForProduct, isTaxExempt, discountPercentage);
            var entry = sourceEntry.Clone() as Entry;

            _sourceEntries.Add(entry);
            ProcessEntries(_sourceEntries);

            return entry;
        }

        public void Register(Entry entry) => Add(new KeyValuePair<Guid, Entry>(entry.Id, entry));

        public void Unregister(Entry entry)
        {
            System.Diagnostics.Debug.Assert(entry != null);

            Remove(new KeyValuePair<Guid, Entry>(entry.Id, entry));

            if (Count == 0 && RegistryCleared != null) { RegistryCleared(); }
        }

        public void UnregisterAll()
        {
            foreach (var kv in this) { Unregister(kv.Value); }
        }

        public void Unregister(Guid productId, decimal taxRate, decimal productMarkupPrice, List<Entry> entries)
        {
            System.Diagnostics.Debug.Assert(taxRate >= 0);
            System.Diagnostics.Debug.Assert(productMarkupPrice >= 0);

            var entry = Lookup(productId, taxRate, productMarkupPrice);
            System.Diagnostics.Debug.Assert(entry != null);

            Unregister(entry);

            var removeEntry = entries.First(entryItem => entryItem.Id == productId && entryItem.TaxPercentage == taxRate && Math.Round(entryItem.CurrentMarkupPrice, 2) == productMarkupPrice);
            entries.Remove(removeEntry);
        }

        public Entry Lookup(Guid productId, decimal taxPercentage, decimal productMarkupPrice)
        {
            var keyValuePair = this.FirstOrDefault(kv =>
                                        kv.Value.Id == productId &&
                                        kv.Value.TaxPercentage == taxPercentage &&
                                        Math.Round(kv.Value.CurrentMarkupPrice, 2) == productMarkupPrice);

            return keyValuePair.Value;
        }

        public Entry Lookup(Guid productId, decimal taxPercentage)
        {
            var keyValuePair = this.FirstOrDefault(kv =>
                                        kv.Value.Id == productId &&
                                        kv.Value.TaxPercentage == taxPercentage);
            return keyValuePair.Value;
        }
    }
}