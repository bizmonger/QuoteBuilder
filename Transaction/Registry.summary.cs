using System;
using System.Linq;

namespace Transaction
{
    public partial class Registry
    {
        public decimal Subtotal()
        {
            if (this.Count == 0) { return 0; }

            decimal subtotal = 0;

            foreach (var item in this.Select(item => item).ToList())
            {
                var entry = item.Value;
                entry.Validate();

                subtotal += entry.DiscountedPrice();
            }

            return subtotal;
        }

        public decimal Total()
        {
            decimal subtotal = Subtotal();
            decimal taxAccumulated = Tax();

            return Math.Round(subtotal + taxAccumulated, 2);
        }

        public decimal Tax()
        {
            var enumerator = GetEnumerator();
            decimal taxAccumulated = 0;

            while (enumerator.MoveNext())
            {
                var keyValue = enumerator.Current;
                Entry entry = keyValue.Value;

                decimal finalPriceEntry = entry.DiscountedPrice();
                decimal taxAmount = 0;

                if (!entry.IsTaxExempt)
                {
                    decimal taxRate = entry.TaxPercentage * .01M;
                    taxAmount = finalPriceEntry * taxRate;
                }

                taxAccumulated += taxAmount;
            }

            return taxAccumulated;
        }

        public decimal Spare()
        {
            decimal spare = 0;

            foreach (var item in this)
            {
                var entry = item.Value;
                spare += (entry.Quantity) * entry.Spare;
            }

            return spare;
        }

        public void Summary(out decimal subtotal, out decimal total, out decimal tax, out decimal totalSpare)
        {
            subtotal = total = tax = totalSpare = 0;

            subtotal = Subtotal();
            total = Total();
            tax = total - subtotal;
            totalSpare = Spare();
        }
    }
}