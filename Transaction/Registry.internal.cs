namespace Transaction
{
    public partial class Registry
    {
        bool IsUniqueEntry(Entry registeredEntry, Entry entry) => registeredEntry.DiscountPercentage.Equals(entry.DiscountPercentage) &&
            registeredEntry.CurrentMarkupPrice.Equals(entry.CurrentMarkupPrice) &&
            registeredEntry.TaxPercentage.Equals(entry.TaxPercentage);
    }
}