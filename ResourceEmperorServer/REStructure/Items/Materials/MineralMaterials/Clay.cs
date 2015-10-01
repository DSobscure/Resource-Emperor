using REProtocol;

namespace REStructure.Items.Materials
{
    public class Clay : MineralMaterial
    {
        public Clay() : base() { }
        public Clay(int itemCount) : base(itemCount)
        {
            id = ItemID.Clay;
            name = "黏土";
            description = "";
            type = (int)MineralType.Soil;
        }
    }
}
