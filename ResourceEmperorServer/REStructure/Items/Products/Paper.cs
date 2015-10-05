using REProtocol;

namespace REStructure.Items.Products
{
    public class Paper : Product
    {
        static ItemID _id;
        static string _name;
        static string _description;

        static Paper()
        {
            _id = ItemID.Paper;
            _name = "紙";
            _description = "";
        }
        protected Paper() { }
        public Paper(int itemCount) : base(itemCount) { }

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
            return new Paper(itemCount);
        }
    }
}
