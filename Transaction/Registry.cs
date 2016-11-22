using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Transaction
{
    [DebuggerNonUserCode]
    public partial class Registry : List<KeyValuePair<Guid, Entry>>
    {
        public Registry() { }
        public Registry(IClient client) { client.DiscountApplied += (sender, discountPercentage) => ApplyDiscount(discountPercentage); }
    }
}