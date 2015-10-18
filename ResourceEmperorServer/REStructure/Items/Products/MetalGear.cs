using REProtocol;

namespace REStructure.Items.Products
{
    public class MetalGear : Product
    {
        static ItemID _id;
        static string _name;
        static string _description;

        static MetalGear()
        {
            _id = ItemID.MetalGear;
            _name = "金屬齒輪";
            _description = "";
        }
        protected MetalGear() { }
        public MetalGear(int itemCount) : base(itemCount) { }

        public override ItemID id
        {
            get
            {
                return _id;
            }
        }

        public override string name
        {
            get
            {
                return _name;
            }
        }

        public override string description
        {
            get
            {
                return _description;
            }
        }

        public override object Clone()
        {
            return new MetalGear(itemCount);
        }
    }
}
