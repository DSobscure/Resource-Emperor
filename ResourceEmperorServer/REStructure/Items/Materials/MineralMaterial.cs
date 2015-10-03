using REProtocol;

namespace REStructure.Items.Materials
{
    public abstract class MineralMaterial : Material
    {
        public abstract MineralType type { get; protected set; }
        protected MineralMaterial() : base() { }
        protected MineralMaterial(int itemCount) : base(itemCount)
        {

        }
    }
}
