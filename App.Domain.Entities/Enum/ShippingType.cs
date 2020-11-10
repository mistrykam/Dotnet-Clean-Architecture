using App.Domain.Entities.Framework;

namespace App.Domain.Entities.Enum
{
    public abstract class ShippingType : Enumeration
    {
        public static readonly ShippingType RegularMail = new RegularMailType();
        public static readonly ShippingType ExpressMail = new ExpressMailType();
        public static readonly ShippingType OvernightMail = new OvernighMailType();

        protected ShippingType() { }
        protected ShippingType(int value, string displayName) : base(value, displayName) { }

        public abstract decimal ShippingCost { get; }

        // Shipping types

        private class RegularMailType : ShippingType
        {
            public RegularMailType() : base(0, "Regular Mail") { }

            public override decimal ShippingCost
            {
                get { return 9.95m; }
            }
        }

        private class ExpressMailType : ShippingType
        {
            public ExpressMailType() : base(0, "Express Mail") { }

            public override decimal ShippingCost
            {
                get { return 19.95m; }
            }
        }

        private class OvernighMailType : ShippingType
        {
            public OvernighMailType() : base(0, "Overnight Mail") { }

            public override decimal ShippingCost
            {
                get { return 39.95m; }
            }
        }
    }
}
