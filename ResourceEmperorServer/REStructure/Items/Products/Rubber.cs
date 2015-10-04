using REProtocol;

namespace REStructure.Items.Products
{
    public class Rubber : Product
    {
        static ItemID _id;
        static string _name;
        static string _description;

        static Rubber()
        {
            _id = ItemID.Rubber;
            _name = "橡膠";
            _description = "";
        }
        protected Rubber() { }
        public Rubber(int itemCount) : base(itemCount) { }

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
