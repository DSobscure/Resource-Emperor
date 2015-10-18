using REProtocol;

namespace REStructure.Items.Materials
{
    public class Rock : MineralMaterial
    {
        static ItemID _id;
        static string _name;
        static string _description;
        static MineralType _type;

        static Rock()
        {
            _id = ItemID.Rock;
            _name = "石頭";
            _description = "";
            _type = MineralType.Stone;
        }
        protected Rock() { }
        public Rock(int itemCount) : base(itemCount) { }

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
            return new Rock(itemCount);
        }
    }
}
