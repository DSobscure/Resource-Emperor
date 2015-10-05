using REProtocol;

namespace REStructure.Items.Products
{
    public class Cottonwool : Product
    {
        static ItemID _id;
        static string _name;
        static string _description;

        static Cottonwool()
        {
            _id = ItemID.Cottonwool;
            _name = "棉絮";
            _description = "";
        }
        protected Cottonwool() { }
        public Cottonwool(int itemCount) : base(itemCount) { }

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
            return new Cottonwool(itemCount);
        }
    }
}
