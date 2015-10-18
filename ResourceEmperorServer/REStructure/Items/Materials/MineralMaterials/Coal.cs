using REProtocol;

namespace REStructure.Items.Materials
{
    public class Coal : MineralMaterial
    {
        static ItemID _id;
        static string _name;
        static string _description;
        static MineralType _type;

        static Coal()
        {
            _id = ItemID.Coal;
            _name = "煤礦";
            _description = "";
            _type = MineralType.Fossil;
        }
        protected Coal() : base() { }
        public Coal(int itemCount) : base(itemCount) { }

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

        public override MineralType type
        {
            get
            {
                return _type;
            }

            protected set
            {
                _type = value;
            }
        }

        public override object Clone()
        {
            return new Coal(itemCount);
        }
    }
}
