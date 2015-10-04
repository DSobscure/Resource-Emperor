using REProtocol;

namespace REStructure.Items.Products
{
    public class WoodenAxle : Product
    {
        static ItemID _id;
        static string _name;
        static string _description;

        static WoodenAxle()
        {
            _id = ItemID.WoodenAxle;
            _name = "木製輪軸";
            _description = "";
        }
        protected WoodenAxle() { }
        public WoodenAxle(int itemCount) : base(itemCount) { }

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
