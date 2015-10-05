using REProtocol;

namespace REStructure.Items.Tools
{
    public class StoneAxe : Tool
    {
        static ItemID _id;
        static string _name;
        static string _description;

        static StoneAxe()
        {
            _id = ItemID.StoneAxe;
            _name = "石斧";
            _description = "";
        }
        public StoneAxe() { }
        public StoneAxe(int itemCount, int durability, int durabilityLimit) : base(itemCount, durability, durabilityLimit) { }

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
            return new StoneAxe(itemCount,durability,durabilityLimit);
        }
    }
}
