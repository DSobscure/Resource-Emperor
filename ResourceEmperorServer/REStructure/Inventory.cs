using System.Collections;
using System.Collections.Generic;
using RESerializable;
using System.Text;
using REStructure.Items.Materials;
using REStructure.Items.Products;
using REStructure.Items.Tools;
using REStructure.Items;
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
                if(item is Tool)
                {
                    Tool tool = item as Tool;
                    sb.AppendFormat("{0},{1},{2},{3}\n", ((int)tool.id).ToString(), tool.itemCount.ToString(),tool.durability.ToString(), tool.durabilityLimit.ToString());
                }
                else
                {
                    sb.AppendFormat("{0},{1}\n", (int)item.id, item.itemCount.ToString());
                }
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
                    int itemCount = int.Parse(data[1]);
                    int durability = 0, durabilityLimit = 0;
                    if(data.Length >= 4)
                    {
                        durability = int.Parse(data[2]);
                        durabilityLimit = int.Parse(data[3]);
                    }
                    switch ((ItemID)int.Parse(data[0]))
                    {
                        #region materials
                        #region plant materials
                        case ItemID.Log:
                            {
                                items.Add(ItemID.Log, new Log(itemCount));
                            }
                            break;
                        case ItemID.Oak:
                            {
                                items.Add(ItemID.Oak, new Oak(itemCount));
                            }
                            break;
                        case ItemID.Cypress:
                            {
                                items.Add(ItemID.Cypress, new Cypress(itemCount));
                            }
                            break;
                        case ItemID.Bamboo:
                            {
                                items.Add(ItemID.Bamboo, new Bamboo(itemCount));
                            }
                            break;
                        case ItemID.Hemp:
                            {
                                items.Add(ItemID.Hemp, new Hemp(itemCount));
                            }
                            break;
                        case ItemID.Cotton:
                            {
                                items.Add(ItemID.Cotton, new Cotton(itemCount));
                            }
                            break;
                        case ItemID.RawRubber:
                            {
                                items.Add(ItemID.RawRubber, new RawRubber(itemCount));
                            }
                            break;
                        #endregion
                        #region mineral materials
                        case ItemID.Clay:
                            {
                                items.Add(ItemID.Clay, new Clay(itemCount));
                            }
                            break;
                        case ItemID.Rock:
                            {
                                items.Add(ItemID.Rock, new Rock(itemCount));
                            }
                            break;
                        case ItemID.Sand:
                            {
                                items.Add(ItemID.Sand, new Sand(itemCount));
                            }
                            break;
                        case ItemID.IronOre:
                            {
                                items.Add(ItemID.IronOre, new IronOre(itemCount));
                            }
                            break;
                        case ItemID.CopperOre:
                            {
                                items.Add(ItemID.CopperOre, new CopperOre(itemCount));
                            }
                            break;
                        case ItemID.Coal:
                            {
                                items.Add(ItemID.Coal, new Coal(itemCount));
                            }
                            break;
                        #endregion
                        #region liquid materials
                        case ItemID.Brine:
                            {
                                items.Add(ItemID.Brine, new Brine(itemCount));
                            }
                            break;
                        case ItemID.Water:
                            {
                                items.Add(ItemID.Water, new Water(itemCount));
                            }
                            break;
                        #endregion
                        #endregion
                        #region products
                        case ItemID.Barrel:
                            {
                                items.Add(ItemID.Barrel, new Barrel(itemCount));
                            }
                            break;
                        case ItemID.Blade:
                            {
                                items.Add(ItemID.Blade, new Blade(itemCount));
                            }
                            break;
                        case ItemID.Brick:
                            {
                                items.Add(ItemID.Brick, new Brick(itemCount));
                            }
                            break;
                        case ItemID.Charcoal:
                            {
                                items.Add(ItemID.Charcoal, new Charcoal(itemCount));
                            }
                            break;
                        case ItemID.CircularSawBlade:
                            {
                                items.Add(ItemID.CircularSawBlade, new CircularSawBlade(itemCount));
                            }
                            break;
                        case ItemID.Copper:
                            {
                                items.Add(ItemID.Copper, new Copper(itemCount));
                            }
                            break;
                        case ItemID.CopperBlock:
                            {
                                items.Add(ItemID.CopperBlock, new CopperBlock(itemCount));
                            }
                            break;
                        case ItemID.CopperSheet:
                            {
                                items.Add(ItemID.CopperSheet, new CopperSheet(itemCount));
                            }
                            break;
                        case ItemID.CottonCloth:
                            {
                                items.Add(ItemID.CottonCloth, new CottonCloth(itemCount));
                            }
                            break;
                        case ItemID.CottonRope:
                            {
                                items.Add(ItemID.CottonRope, new CottonRope(itemCount));
                            }
                            break;
                        case ItemID.CottonThread:
                            {
                                items.Add(ItemID.CottonThread, new CottonThread(itemCount));
                            }
                            break;
                        case ItemID.Cottonwool:
                            {
                                items.Add(ItemID.Cottonwool, new Cottonwool(itemCount));
                            }
                            break;
                        case ItemID.CypressTimber:
                            {
                                items.Add(ItemID.CypressTimber, new CypressTimber(itemCount));
                            }
                            break;
                        case ItemID.Firewood:
                            {
                                items.Add(ItemID.Firewood, new Firewood(itemCount));
                            }
                            break;
                        case ItemID.HempRope:
                            {
                                items.Add(ItemID.HempRope, new HempRope(itemCount));
                            }
                            break;
                        case ItemID.IronBar:
                            {
                                items.Add(ItemID.IronBar, new IronBar(itemCount));
                            }
                            break;
                        case ItemID.IronBlock:
                            {
                                items.Add(ItemID.IronBlock, new IronBlock(itemCount));
                            }
                            break;
                        case ItemID.IronPipe:
                            {
                                items.Add(ItemID.IronPipe, new IronPipe(itemCount));
                            }
                            break;
                        case ItemID.IronSheet:
                            {
                                items.Add(ItemID.IronSheet, new IronSheet(itemCount));
                            }
                            break;
                        case ItemID.MetalGear:
                            {
                                items.Add(ItemID.MetalGear, new MetalGear(itemCount));
                            }
                            break;
                        case ItemID.OakTimber:
                            {
                                items.Add(ItemID.OakTimber, new OakTimber(itemCount));
                            }
                            break;
                        case ItemID.Paper:
                            {
                                items.Add(ItemID.Paper, new Paper(itemCount));
                            }
                            break;
                        case ItemID.Rivet:
                            {
                                items.Add(ItemID.Rivet, new Rivet(itemCount));
                            }
                            break;
                        case ItemID.Rubber:
                            {
                                items.Add(ItemID.Rubber, new Rubber(itemCount));
                            }
                            break;
                        case ItemID.StoneBlade:
                            {
                                items.Add(ItemID.StoneBlade, new StoneBlade(itemCount));
                            }
                            break;
                        case ItemID.Timber:
                            {
                                items.Add(ItemID.Timber, new Timber(itemCount));
                            }
                            break;
                        case ItemID.WoodenAxle:
                            {
                                items.Add(ItemID.WoodenAxle, new WoodenAxle(itemCount));
                            }
                            break;
                        case ItemID.WroughIron:
                            {
                                items.Add(ItemID.WroughIron, new WroughIron(itemCount));
                            }
                            break;
                        #endregion
                        #region tools
                        case ItemID.StoneAxe:
                            {
                                items.Add(ItemID.StoneAxe, new StoneAxe(itemCount, durability, durabilityLimit));
                            }
                            break;
                        case ItemID.StonePickaxe:
                            {
                                items.Add(ItemID.StonePickaxe, new StonePickaxe(itemCount, durability, durabilityLimit));
                            }
                            break;
                        case ItemID.StoneShovel:
                            {
                                items.Add(ItemID.StoneShovel, new StoneShovel(itemCount, durability, durabilityLimit));
                            }
                            break;
                        #endregion
                    }
                }
            }
        }
    }
}
