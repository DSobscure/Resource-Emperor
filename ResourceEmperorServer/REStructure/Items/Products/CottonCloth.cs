using REProtocol;

namespace REStructure.Items.Products
{
    public class CottonCloth : Product
    {
        static ItemID _id;
        static string _name;
        static string _description;

        static CottonCloth()
        {
            _id = ItemID.CottonCloth;
            _name = "棉布";
            _description = "";
        }
        protected CottonCloth() { }
        public CottonCloth(int itemCount) : base(itemCount) { }

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
            return new CottonCloth(itemCount);
        }
    }
}
