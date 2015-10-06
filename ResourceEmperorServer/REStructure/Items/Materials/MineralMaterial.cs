﻿using REProtocol;
using Newtonsoft.Json;

namespace REStructure.Items.Materials
{
    public abstract class MineralMaterial : Material
    {
        [JsonProperty("type")]
        public abstract MineralType type { get; protected set; }
        protected MineralMaterial() : base() { }
        protected MineralMaterial(int itemCount) : base(itemCount)
        {

        }
    }
}
