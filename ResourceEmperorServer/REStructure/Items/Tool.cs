using System;
using REProtocol;
using Newtonsoft.Json;

namespace REStructure.Items
{
    public abstract class Tool : Item
    {
        static int _maxCount;

        [JsonProperty("durability")]
        public int durability { get;protected set; }
        [JsonProperty("durabilityLimit")]
        public int durabilityLimit { get;protected set; }

        static Tool()
        {
            _maxCount = 1;
        }

        protected Tool() { }
        protected Tool(int itemCount, int durability, int durabilityLimit) : base(itemCount)
        {
            this.durability = durability;
            this.durabilityLimit = durabilityLimit;
        }

        public override int maxCount
        {
            get
            {
                return _maxCount;
            }
        }
    }
}
