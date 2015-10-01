using REProtocol;

namespace REStructure.Items.Materials
{
    public class Sand : MineralMaterial
    {
        public Sand() : base() { }
        public Sand(int itemCount) : base(itemCount)
        {
            id = ItemID.Sand;
            name = "沙";
            description = "";
            type = MineralType.Stone;
        }
    }
}
