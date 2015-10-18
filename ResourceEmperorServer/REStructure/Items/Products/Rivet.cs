using REProtocol;

namespace REStructure.Items.Products
{
    public class Rivet : Product
    {
        static ItemID _id;
        static string _name;
        static string _description;

        static Rivet()
        {
            _id = ItemID.Rivet;
            _name = "鉚釘";
            _description = "";
        }
        protected Rivet() { }
        public Rivet(int itemCount) : base(itemCount) { }

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
            return new Rivet(itemCount);
        }
    }
}
