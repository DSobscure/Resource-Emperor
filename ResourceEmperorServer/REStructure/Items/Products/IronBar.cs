using REProtocol;

namespace REStructure.Items.Products
{
    public class IronBar : Product
    {
        static ItemID _id;
        static string _name;
        static string _description;

        static IronBar()
        {
            _id = ItemID.IronBar;
            _name = "鐵條";
            _description = "";
        }
        protected IronBar() { }
        public IronBar(int itemCount) : base(itemCount) { }

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

        public override object Clone()
        {
            return new IronBar(itemCount);
        }
    }
}
