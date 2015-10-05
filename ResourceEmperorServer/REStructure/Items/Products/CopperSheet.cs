using REProtocol;

namespace REStructure.Items.Products
{
    public class CopperSheet : Product
    {
        static ItemID _id;
        static string _name;
        static string _description;

        static CopperSheet()
        {
            _id = ItemID.CopperSheet;
            _name = "銅片";
            _description = "";
        }
        protected CopperSheet() { }
        public CopperSheet(int itemCount) : base(itemCount) { }

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
            return new CopperSheet(itemCount);
        }
    }
}
