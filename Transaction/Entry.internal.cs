namespace Transaction
{
    public partial class Entry
    {
        decimal ApplyEntryDiscount(decimal entryAmount)
        {
            decimal entryDiscountRate = DiscountPercentage * .01M;
            decimal entryDiscountedAmount = entryAmount * entryDiscountRate;

            return entryDiscountedAmount;
        }

        decimal CalculateEntryAmount() => CurrentMarkupPrice * Quantity;
    }
}