using System;
using System.Diagnostics;
using System.Runtime.Serialization;


namespace Transaction.Exceptions
{
    [DebuggerNonUserCode]
    [DataContract]
    public class ZeroQuantityException : Exception
    {
        public ZeroQuantityException() : base() { }
        public ZeroQuantityException(string message) : base(message) { }
        public ZeroQuantityException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected ZeroQuantityException(StreamingContext context) { }
    }
    [DebuggerNonUserCode]
    [DataContract]
    public class ProductPriceException : Exception
    {
        public ProductPriceException() : base() { }
        public ProductPriceException(string message) : base(message) { }
        public ProductPriceException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected ProductPriceException(StreamingContext context) { }
    }
    [DebuggerNonUserCode]
    [DataContract]
    public class ProductMarkupPriceException : Exception
    {
        public ProductMarkupPriceException() : base() { }
        public ProductMarkupPriceException(string message) : base(message) { }
        public ProductMarkupPriceException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected ProductMarkupPriceException(StreamingContext context) { }
    }
    [DebuggerNonUserCode]
    [DataContract]
    public class ProductMinimumPriceException : Exception
    {
        public ProductMinimumPriceException() : base() { }
        public ProductMinimumPriceException(string message) : base(message) { }
        public ProductMinimumPriceException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected ProductMinimumPriceException(StreamingContext context) { }
    }
    [DebuggerNonUserCode]
    [DataContract]
    public class QuantityDiscountedException : Exception
    {
        public QuantityDiscountedException() : base() { }
        public QuantityDiscountedException(string message) : base(message) { }
        public QuantityDiscountedException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected QuantityDiscountedException(StreamingContext context) { }
    }
    [DebuggerNonUserCode]
    [DataContract]
    public class TaxRateException : Exception
    {
        public TaxRateException() : base() { }
        public TaxRateException(string message) : base(message) { }
        public TaxRateException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected TaxRateException(StreamingContext context) { }
    }

    [DebuggerNonUserCode]
    [DataContract]
    public class InconsistentDiscountsException : Exception
    {
        public InconsistentDiscountsException() : base() { }
        public InconsistentDiscountsException(string message) : base(message) { }
        public InconsistentDiscountsException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected InconsistentDiscountsException(StreamingContext context) { }
    }

    [DebuggerNonUserCode]
    [DataContract]
    public class InconsistentTaxRateException : Exception
    {
        public InconsistentTaxRateException() : base() { }
        public InconsistentTaxRateException(string message) : base(message) { }
        public InconsistentTaxRateException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected InconsistentTaxRateException(StreamingContext context) { }
    }
}