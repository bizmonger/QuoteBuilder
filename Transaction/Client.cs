using System.Diagnostics;

namespace Transaction
{
    [DebuggerNonUserCode]
    public class Client : IClient
    {
        public event DiscountHandler DiscountApplied;

        public void ApplyDiscount(Registry registry, decimal discountPercentage)
        {
            DiscountApplied(registry, discountPercentage);
        }
    }
}