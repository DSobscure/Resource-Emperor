using REProtocol;

namespace REStructure.Items.Products
{
    public class Barrel : Product
    {
        static ItemID _id;
        static string _name;
        static string _description;

        static Barrel()
        {
            _id = ItemID.Barrel;
            _name = "木桶";
            _description = "";
        }
        protected Barrel() { }
        public Barrel(int itemCount) : base(itemCount) { }

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
            return new Barrel(itemCount);
        }
    }
}
