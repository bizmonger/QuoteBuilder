using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using Transaction.Exceptions;

namespace Transaction
{
    [DebuggerNonUserCode]
    [DataContract]
    public partial class Entry
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        public decimal Quantity { get; set; }
        public decimal MinimumPrice { get; set; }
        public decimal OriginalMarkupPrice { get; set; }
        public decimal CurrentMarkupPrice { get; set; }
        public decimal TaxPercentage { get; set; }
        public decimal Tax => Math.Round(DiscountedPrice() * (TaxPercentage * .01M), 2);
        public decimal SubtotalAppliedDiscountedPrice { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal EntrySubtotalDiscountPercentage { get; set; }
        public bool IsExchange { get; set; }
        public bool IsTaxExempt { get; set; }

        public static Entry Create(Guid productID, decimal minimumPrice, decimal quantity, decimal price, decimal taxRateForProduct, bool isTaxExempt, decimal discountPercentage = 0) => new Entry()
        {
            Id = productID,
            CurrentMarkupPrice = quantity > 0 ? price : 0,
            OriginalMarkupPrice = price,
            DiscountPercentage = discountPercentage,
            Quantity = quantity,
            TaxPercentage = taxRateForProduct * 100,
            MinimumPrice = minimumPrice,
            IsTaxExempt = isTaxExempt,
        };

        public decimal DiscountedPrice()
        {
            decimal entryAmount = CalculateEntryAmount();
            decimal entryDiscountedAmount = ApplyEntryDiscount(entryAmount);

            entryAmount -= entryDiscountedAmount;
            entryAmount = !IsExchange ? entryAmount : entryAmount * -1;

            return entryAmount;
        }
        
        public void Validate()
        {
            if (!(MinimumPrice >= 0))
            {
                throw new ProductMinimumPriceException("Unable to register sale entry with product minimum price less than zero.");
            }

            if (!(SubtotalAppliedDiscountedPrice >= 0))
            {
                throw new QuantityDiscountedException("Unable to register sale entry with quantity discount less than zero.");
            }

            if (!(TaxPercentage >= 0))
            {
                throw new TaxRateException("Unable to register sale entry with tax rate less than zero.");
            }
        }

        public decimal Spare => CurrentMarkupPrice - MinimumPrice;

        public Entry Clone() => new Entry()
        {
            CurrentMarkupPrice = this.CurrentMarkupPrice,
            DiscountPercentage = this.DiscountPercentage,
            EntrySubtotalDiscountPercentage = this.EntrySubtotalDiscountPercentage,
            IsExchange = this.IsExchange,
            IsTaxExempt = this.IsTaxExempt,
            MinimumPrice = this.MinimumPrice,
            OriginalMarkupPrice = this.OriginalMarkupPrice,
            Id = this.Id,
            Quantity = this.Quantity,
            SubtotalAppliedDiscountedPrice = this.SubtotalAppliedDiscountedPrice,
            TaxPercentage = this.TaxPercentage
        };
    }
}