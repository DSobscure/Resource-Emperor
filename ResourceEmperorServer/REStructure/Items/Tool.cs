using System;
using REProtocol;

namespace REStructure.Items
{
    public abstract class Tool : Item
    {
        public int durability { get; set; }
        public int durabilityLimit { get; set; }
        public int durabilityCost { get; set; }

        public Tool(int itemCount, int durability, int durabilityLimit, int durabilityCost) : base(itemCount)
        {
            this.durability = durability;
            this.durabilityLimit = durabilityLimit;
            this.durabilityCost = durabilityCost;
        }
    }
}
