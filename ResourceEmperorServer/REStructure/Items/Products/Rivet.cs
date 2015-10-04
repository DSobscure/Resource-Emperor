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
