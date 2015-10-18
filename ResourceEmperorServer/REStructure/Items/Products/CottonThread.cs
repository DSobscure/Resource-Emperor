using REProtocol;

namespace REStructure.Items.Products
{
    public class CottonThread : Product
    {
        static ItemID _id;
        static string _name;
        static string _description;

        static CottonThread()
        {
            _id = ItemID.CottonThread;
            _name = "棉線";
            _description = "";
        }
        protected CottonThread() { }
        public CottonThread(int itemCount) : base(itemCount) { }

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
            return new CottonThread(itemCount);
        }
    }
}
