using REProtocol;

namespace REStructure.Items.Materials
{
    public class Coal : MineralMaterial
    {
        public Coal() : base() { }
        public Coal(int itemCount) : base(itemCount)
        {
            id = ItemID.Coal;
            name = "煤礦";
            description = "";
            type = MineralType.Fossil;
        }
    }
}
