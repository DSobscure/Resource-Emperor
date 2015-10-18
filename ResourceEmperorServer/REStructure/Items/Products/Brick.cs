using REProtocol;

namespace REStructure.Items.Products
{
    public class Brick : Product
    {
        static ItemID _id;
        static string _name;
        static string _description;

        static Brick()
        {
            _id = ItemID.Brick;
            _name = "磚塊";
            _description = "";
        }
        protected Brick() { }
        public Brick(int itemCount) : base(itemCount) { }

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
            return new Brick(itemCount);
        }
    }
}
