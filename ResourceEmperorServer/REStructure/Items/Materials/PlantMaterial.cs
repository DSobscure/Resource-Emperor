using System;
using REProtocol;

namespace REStructure.Items.Materials
{
    public abstract class PlantMaterial : Material
    {
        public PlantType type { get; protected set; }
        protected PlantMaterial() : base() { }
        protected PlantMaterial(int itemCount) : base(itemCount)
        {

        }
    }
}
