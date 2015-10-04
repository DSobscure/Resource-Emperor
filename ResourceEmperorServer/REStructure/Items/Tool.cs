using System;
using REProtocol;

namespace REStructure.Items
{
    public abstract class Tool : Item
    {
        public int durability { get;protected set; }
        public int durabilityLimit { get;protected set; }

        protected Tool() { }
        protected Tool(int itemCount, int durability, int durabilityLimit) : base(itemCount)
        {
            this.durability = durability;
            this.durabilityLimit = durabilityLimit;
        }
    }
}
