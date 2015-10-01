using REProtocol;

namespace REStructure.Items.Materials
{
    public class Rubber : PlantMaterial
    {
        public Rubber() : base() { }
        public Rubber(int itemCount) : base(itemCount)
        {
            id = ItemID.Rubber;
            name = "生橡膠";
            description = "";
            type = PlantType.Product;
        }
    }
}
