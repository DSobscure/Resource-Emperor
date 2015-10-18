using REProtocol;

namespace REStructure.Items.Products
{
    public class IronSheet : Product
    {
        static ItemID _id;
        static string _name;
        static string _description;

        static IronSheet()
        {
            _id = ItemID.IronSheet;
            _name = "鐵皮";
            _description = "";
        }
        protected IronSheet() { }
        public IronSheet(int itemCount) : base(itemCount) { }

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
            return new IronSheet(itemCount);
        }
    }
}
