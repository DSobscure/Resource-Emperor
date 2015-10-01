using System;
using REProtocol;

namespace REStructure.Items.Materials
{
    public abstract class AnimalMaterial : Material
    {
        public AnimalType type { get; protected set; }
        protected AnimalMaterial() : base() { }
        protected AnimalMaterial(int itemCount) : base(itemCount)
        {

        }
    }
}
