using REProtocol;

namespace REStructure.Items.Materials
{
    public class Hemp : PlantMaterial
    {
        public Hemp() : base() { }
        public Hemp(int itemCount) : base(itemCount)
        {
            id = ItemID.Hemp;
            name = "大麻";
            description = "";
            type = PlantType.Herb;
        }
    }
}
