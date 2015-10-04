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

            protected set
            {
                _id = value;
            }
        }

        public override string name
        {
            get
            {
                return _name;
            }

            protected set
            {
                _name = value;
            }
        }

        public override string description
        {
            get
            {
                return _description;
            }

            protected set
            {
                _description = value;
            }
        }
    }
}
