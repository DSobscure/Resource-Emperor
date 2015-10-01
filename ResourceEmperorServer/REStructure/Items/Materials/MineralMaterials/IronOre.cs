using System;
using REProtocol;

namespace REStructure.Items.Materials
{
    public class IronOre : MineralMaterial
    {
        public IronOre() : base() { }
        public IronOre(int itemCount) : base(itemCount)
        {
            id = ItemID.IronOre;
            name = "鐵礦";
            description = "";
            type = MineralType.Mine;
        }
    }
}
