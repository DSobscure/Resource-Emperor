using REProtocol;

namespace REStructure.Items.Materials
{
    public class CopperOre : MineralMaterial
    {
        public CopperOre() : base() { }
        public CopperOre(int itemCount) : base(itemCount)
        {
            id = ItemID.CopperOre;
            name = "銅礦";
            description = "";
            type = MineralType.Mine;
        }
    }
}
