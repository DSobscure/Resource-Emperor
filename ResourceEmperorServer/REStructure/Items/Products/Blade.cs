using REProtocol;

namespace REStructure.Items.Products
{
    public class Blade : Product
    {
        static ItemID _id;
        static string _name;
        static string _description;

        static Blade()
        {
            _id = ItemID.Blade;
            _name = "刀片";
            _description = "";
        }
        protected Blade() { }
        public Blade(int itemCount) : base(itemCount) { }

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
            return new Blade(itemCount);
        }
    }
}
