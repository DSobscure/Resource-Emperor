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
            return new Rubber(itemCount);
        }
    }
}
