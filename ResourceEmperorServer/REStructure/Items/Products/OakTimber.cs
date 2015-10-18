using REProtocol;

namespace REStructure.Items.Products
{
    public class OakTimber : Product
    {
        static ItemID _id;
        static string _name;
        static string _description;

        static OakTimber()
        {
            _id = ItemID.OakTimber;
            _name = "橡木材";
            _description = "";
        }
        protected OakTimber() { }
        public OakTimber(int itemCount) : base(itemCount) { }

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
            return new OakTimber(itemCount);
        }
    }
}
