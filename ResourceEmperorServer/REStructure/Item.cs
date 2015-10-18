using System;
using REProtocol;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace REStructure
{
    public abstract class Item : IComparable<Item> , IScalable , ICloneable
    {
        [JsonIgnore]
        public abstract ItemID id { get;}
        [JsonIgnore]
        public abstract string name { get;}
        [JsonIgnore]
        public abstract string description { get;}
        [JsonProperty("ietmCount")]
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
            if(value > 0)
                itemCount += value;
        }

        public void Decrease(int value = 1)
        {
            if (value > 0)
                itemCount -= value;
        }

        public void Reset()
        {
            itemCount = 0;
        }

        public abstract object Clone();

        public override bool Equals(object obj)
        {
            if(obj is Item)
            {
                Item i = obj as Item;
                return id == i.id;
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            return id.GetHashCode();
        }
    }
}
