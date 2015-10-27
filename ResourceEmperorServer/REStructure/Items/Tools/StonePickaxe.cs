using REProtocol;

namespace REStructure.Items.Tools
{
    public class StonePickaxe : Pickaxe
    {
        static ItemID _id;
        static string _name;
        static string _description;

        static StonePickaxe()
        {
            _id = ItemID.StonePickaxe;
            _name = "石鎬";
            _description = "";
        }
        public StonePickaxe() { }
        public StonePickaxe(int itemCount, int durability, int durabilityLimit) : base(itemCount, durability, durabilityLimit) { }

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
            return new StonePickaxe(itemCount,durabilityLimit,durabilityLimit);
        }
    }
}
