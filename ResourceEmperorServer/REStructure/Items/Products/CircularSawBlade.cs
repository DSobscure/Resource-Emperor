using REProtocol;

namespace REStructure.Items.Products
{
    public class CircularSawBlade : Product
    {
        static ItemID _id;
        static string _name;
        static string _description;

        static CircularSawBlade()
        {
            _id = ItemID.CircularSawBlade;
            _name = "圓鋸鋸片";
            _description = "";
        }
        protected CircularSawBlade() { }
        public CircularSawBlade(int itemCount) : base(itemCount) { }

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
            return new CircularSawBlade(itemCount);
        }
    }
}
