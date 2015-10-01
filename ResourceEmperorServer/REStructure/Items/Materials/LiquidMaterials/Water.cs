using REProtocol;

namespace REStructure.Items.Materials
{
    public class Water : LiquidMaterial
    {
        public Water() : base() { }
        public Water(int itemCount) : base(itemCount)
        {
            id = ItemID.Water;
            name = "淡水";
            description = "淡水敘述";
            type = LiquidType.Water;
        }
    }
}
