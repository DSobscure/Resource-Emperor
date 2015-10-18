using System;
using REProtocol;
using Newtonsoft.Json;

namespace REStructure.Items.Materials
{
    public abstract class PlantMaterial : Material
    {
        [JsonIgnore]
        public abstract PlantType type { get; protected set; }
        protected PlantMaterial() : base() { }
        protected PlantMaterial(int itemCount) : base(itemCount)
        {

        }
    }
}
