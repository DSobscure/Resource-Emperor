using REProtocol;

namespace REStructure.Items.Products
{
    public class Copper : Product
    {
        static ItemID _id;
        static string _name;
        static string _description;

        static Copper()
        {
            _id = ItemID.Copper;
            _name = "銅材";
            _description = "";
        }
        protected Copper() { }
        public Copper(int itemCount) : base(itemCount) { }

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
            return new Copper(itemCount);
        }
    }
}
