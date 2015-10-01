using System.Collections;
using System.Collections.Generic;
using RESerializable;
using System.Text;
using REStructure.Items.Materials;
using REProtocol;
using System;

namespace REStructure
{
    public class Inventory : IDictionary<ItemID, Item> , IStringSerializable
    {
        protected Dictionary<ItemID, Item> items { get; set; }

        public Inventory()
        {
            items = new Dictionary<ItemID, Item>();
        }
        //IDictionary
        public Item this[ItemID key]
        {
            get
            {
                return items[key];
            }

            set
            {
                items[key] = value;
            }
        }
        public int Count
        {
            get
            {
                return items.Count;
            }
        }
        public bool IsReadOnly
        {
            get
            {
                return true;
            }
        }
        public ICollection<ItemID> Keys
        {
            get
            {
                return items.Keys;
            }
        }
        public ICollection<Item> Values
        {
            get
            {
                return items.Values;
            }
        }
        public void Add(KeyValuePair<ItemID, Item> item)
        {
            items.Add(item.Key, item.Value);
        }
        public void Add(ItemID key, Item value)
        {
            items.Add(key, value);
        }
        public void Clear()
        {
            items.Clear();
        }
        public bool Contains(KeyValuePair<ItemID, Item> item)
        {
            return items.ContainsKey(item.Key) && items[item.Key] == item.Value;
        }
        public bool ContainsKey(ItemID key)
        {
            return items.ContainsKey(key);
        }
        public void CopyTo(KeyValuePair<ItemID, Item>[] array, int arrayIndex)
        {
            for(int i=arrayIndex;i<array.Length;i++)
            {
                items.Add(array[i].Key,array[i].Value);
            }
        }
        public IEnumerator<KeyValuePair<ItemID, Item>> GetEnumerator()
        {
            return items.GetEnumerator();
        }
        public bool Remove(KeyValuePair<ItemID, Item> item)
        {
            return this.Contains(item)&&items.Remove(item.Key);
        }
        public bool Remove(ItemID key)
        {
            return items.Remove(key);
        }
        public bool TryGetValue(ItemID key, out Item value)
        {
            return items.TryGetValue(key, out value);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }
        //IStringSerializable
        public string StringSerialize()
        {
            StringBuilder sb = new StringBuilder();
            foreach(Item item in items.Values)
            {
                sb.AppendFormat("{0},{1}\n",(int)item.id,item.itemCount.ToString());
            }
            return sb.ToString();
        }
        public void StringDeserialize(string toDesetialize)
        {
            items = new Dictionary<ItemID, Item>();
            string[] itemsDataString = toDesetialize.Split('\n');
            foreach(string itemDataString in itemsDataString)
            {
                if(itemDataString.Length>1)
                {
                    string[] data = itemDataString.Split(',');
                    switch ((ItemID)int.Parse(data[0]))
                    {
                        case ItemID.Log:
                            {
                                items.Add(ItemID.Log, new Log(int.Parse(data[1])));
                            }
                            break;
                        case ItemID.Bamboo:
                            {
                                items.Add(ItemID.Bamboo, new Bamboo(int.Parse(data[1])));
                            }
                            break;
                        case ItemID.Hemp:
                            {
                                items.Add(ItemID.Hemp, new Hemp(int.Parse(data[1])));
                            }
                            break;
                        case ItemID.Cotton:
                            {
                                items.Add(ItemID.Cotton, new Cotton(int.Parse(data[1])));
                            }
                            break;
                        case ItemID.Rubber:
                            {
                                items.Add(ItemID.Rubber, new Rubber(int.Parse(data[1])));
                            }
                            break;
                        case ItemID.Clay:
                            {
                                items.Add(ItemID.Clay, new Clay(int.Parse(data[1])));
                            }
                            break;
                        case ItemID.Rock:
                            {
                                items.Add(ItemID.Rock, new Rock(int.Parse(data[1])));
                            }
                            break;
                        case ItemID.Sand:
                            {
                                items.Add(ItemID.Sand, new Sand(int.Parse(data[1])));
                            }
                            break;
                        case ItemID.IronOre:
                            {
                                items.Add(ItemID.IronOre, new IronOre(int.Parse(data[1])));
                            }
                            break;
                        case ItemID.CopperOre:
                            {
                                items.Add(ItemID.CopperOre, new CopperOre(int.Parse(data[1])));
                            }
                            break;
                        case ItemID.Coal:
                            {
                                items.Add(ItemID.Coal, new Coal(int.Parse(data[1])));
                            }
                            break;
                        case ItemID.Brine:
                            {
                                items.Add(ItemID.Brine, new Brine(int.Parse(data[1])));
                            }
                            break;
                        case ItemID.Water:
                            {
                                items.Add(ItemID.Water, new Water(int.Parse(data[1])));
                            }
                            break;
                    }
                }
            }
        }
    }
}
