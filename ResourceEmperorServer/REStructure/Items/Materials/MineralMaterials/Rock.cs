using REProtocol;

namespace REStructure.Items.Materials
{
    public class Rock : MineralMaterial
    {
        public Rock() : base() { }
        public Rock(int itemCount) : base(itemCount)
        {
            id = ItemID.Rock;
            name = "石頭";
            description = "";
            type = MineralType.Stone;
        }
    }
}
