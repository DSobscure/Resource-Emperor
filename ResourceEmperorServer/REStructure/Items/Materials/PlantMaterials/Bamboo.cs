using REProtocol;

namespace REStructure.Items.Materials
{
    public class Bamboo : PlantMaterial
    {
        public Bamboo() : base() { }
        public Bamboo(int itemCount) : base(itemCount)
        {
            id = ItemID.Bamboo;
            name = "竹子";
            description = "";
            type = PlantType.Wood;
        }
    }
}
