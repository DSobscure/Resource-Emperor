using REProtocol;

namespace REStructure.Items.Materials
{
    public class Cotton : PlantMaterial
    {
        public Cotton() : base() { }
        public Cotton(int itemCount) : base(itemCount)
        {
            id = ItemID.Cotton;
            name = "棉花";
            description = "";
            type = PlantType.Product;
        }
    }
}
