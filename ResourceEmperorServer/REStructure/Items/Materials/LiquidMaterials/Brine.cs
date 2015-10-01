using System;
using REProtocol;

namespace REStructure.Items.Materials
{
    public class Brine : LiquidMaterial
    {
        public Brine() : base() { }
        public Brine(int itemCount) : base(itemCount)
        {
            id = ItemID.Brine;
            name = "海水";
            description = "海水敘述";
            type = LiquidType.Water;
        }
    }
}
