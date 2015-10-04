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
