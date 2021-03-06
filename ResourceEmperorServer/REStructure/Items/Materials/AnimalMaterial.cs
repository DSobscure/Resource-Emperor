﻿using System;
using REProtocol;
using Newtonsoft.Json;

namespace REStructure.Items.Materials
{
    public abstract class AnimalMaterial : Material
    {
        [JsonIgnore]
        public abstract AnimalType type { get; protected set; }
        protected AnimalMaterial() : base() { }
        protected AnimalMaterial(int itemCount) : base(itemCount)
        {

        }
    }
}
