using System;
using REProtocol;

namespace REStructure
{
    public abstract class Item : IComparable<Item> , IScalable
    {
        public abstract ItemID id { get; protected set; }
        public abstract string name { get; protected set; }
        public abstract string description { get; protected set; }
        public int itemCount { get; protected set; }

        protected Item() { }
        protected Item(int itemCount)
        {
            this.itemCount = itemCount;
        }
        public int CompareTo(Item other)
        {
            return id.CompareTo(other.id);
        }

        public void Increase(int value = 1)
        {
            itemCount += value;
        }

        public void Decrease(int value = 1)
        {
            itemCount -= value;
        }

        public void Reset()
        {
            itemCount = 0;
        }
    }
}
