using System.Diagnostics;

namespace Transaction
{
    public partial class Registry
    {
        [DebuggerNonUserCode]
        public void ApplyDiscount(decimal discountPercentage)
        {
            System.Diagnostics.Debug.Assert(discountPercentage > 0);

            foreach (var item in this)
            {
                var entry = item.Value;
                entry.EntrySubtotalDiscountPercentage = discountPercentage;
                decimal discountRate = discountPercentage * .01M;
                entry.SubtotalAppliedDiscountedPrice = entry.CurrentMarkupPrice * (1 * discountRate);
                entry.CurrentMarkupPrice -= entry.SubtotalAppliedDiscountedPrice;
            }
        }
    }
}