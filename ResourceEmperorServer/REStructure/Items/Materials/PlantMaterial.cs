using System;
using REProtocol;
using Newtonsoft.Json;

namespace REStructure.Items.Materials
{
    public abstract class PlantMaterial : Material
    {
        [JsonProperty("type")]
        public abstract PlantType type { get; protected set; }
        protected PlantMaterial() : base() { }
        protected PlantMaterial(int itemCount) : base(itemCount)
        {

        }
    }
}
