using System;
using REProtocol;
using Newtonsoft.Json;

namespace REStructure.Items
{
    public abstract class Tool : Item
    {
        [JsonProperty("durability")]
        public int durability { get;protected set; }
        [JsonProperty("durabilityLimit")]
        public int durabilityLimit { get;protected set; }

        protected Tool() { }
        protected Tool(int itemCount, int durability, int durabilityLimit) : base(itemCount)
        {
            this.durability = durability;
            this.durabilityLimit = durabilityLimit;
        }
    }
}
