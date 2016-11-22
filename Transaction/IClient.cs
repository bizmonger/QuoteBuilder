namespace Transaction
{
    public delegate void DiscountHandler(Registry registry, decimal discountPercentage);
    public delegate void CouponAddedHandler(decimal discountPercentage);

    public interface IClient
    {
        event DiscountHandler DiscountApplied;

        void ApplyDiscount(Registry registry, decimal discountPercentage);
    }
}