using REProtocol;

namespace REStructure.Items.Products
{
    public class WroughIron : Product
    {
        static ItemID _id;
        static string _name;
        static string _description;

        static WroughIron()
        {
            _id = ItemID.WroughIron;
            _name = "熟鐵材";
            _description = "";
        }
        protected WroughIron() { }
        public WroughIron(int itemCount) : base(itemCount) { }

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
            return new WroughIron(itemCount);
        }
    }
}
