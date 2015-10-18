using REProtocol;

namespace REStructure.Items.Products
{
    public class Charcoal : Product
    {
        static ItemID _id;
        static string _name;
        static string _description;

        static Charcoal()
        {
            _id = ItemID.Charcoal;
            _name = "木炭";
            _description = "";
        }
        protected Charcoal() { }
        public Charcoal(int itemCount) : base(itemCount) { }

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
            return new Charcoal(itemCount);
        }
    }
}
