using System.Collections;
using System.Collections.Generic;
using REProtocol;
using Newtonsoft.Json;
using System.Linq;
using System;

namespace REStructure
{
    public class Inventory : IList<Item>
    {
        [JsonProperty("items")]
        protected List<Item> items { get; set; }
        [JsonProperty("maxCount")]
        public int maxCount { get; protected set; }
        public event Action OnItemChange;

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
                return false;
            }
        }

        Item IList<Item>.this[int index]
        {
            get
            {
                return items[index];
            }

            set
            {
                items[index] = value;
            }
        }

        public Item this[int index]
        {
            get
            {
                return items[index];
            }
        }

        public Inventory()
        {
            items = new List<Item>();
            maxCount = 40;
        }

        public int IndexOf(Item item)
        {
            return items.IndexOf(item);
        }

        public void Insert(int index, Item item)
        {
            items.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            items.RemoveAt(index);
        }

        public void Add(Item item)
        {
            Stack(item);
        }

        public void Clear()
        {
            items.Clear();
        }

        public bool Contains(Item item)
        {
            return items.Contains(item);
        }

        public void CopyTo(Item[] array, int arrayIndex)
        {
            items.CopyTo(array, arrayIndex);
        }

        public bool Remove(Item item)
        {
            return items.Remove(item);
        }

        public IEnumerator<Item> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }

        public bool Stack(Item item)
        {
            if(items.Any(x=>x.id == item.id && !x.isFull))
            {
                Item targetItem = items.First(x => x.id == item.id && !x.isFull);
                if (item.itemCount <= targetItem.maxCount - targetItem.itemCount)
                {
                    targetItem.Increase(item.itemCount);
                }
                else
                {
                    int addCount = targetItem.maxCount - targetItem.itemCount;
                    targetItem.Increase(addCount);
                    int remainCount = item.itemCount - addCount;
                    while (remainCount > 0)
                    {
                        if (remainCount >= item.maxCount)
                        {
                            if (items.Count >= maxCount)
                                return false;
                            items.Add(item.Instantiate(item.maxCount) as Item);
                            remainCount -= item.maxCount;
                        }
                        else
                        {
                            if (items.Count >= maxCount)
                                return false;
                            items.Add(item.Instantiate(remainCount) as Item);
                            remainCount = 0;
                        }
                    }
                }
            }
            else
            {
                if(item.itemCount > item.maxCount)
                {
                    int remainCount = item.itemCount;
                    while(remainCount > 0)
                    {
                        if(remainCount > item.maxCount)
                        {
                            if (items.Count >= maxCount)
                                return false;
                            items.Add(item.Instantiate(item.maxCount) as Item);
                            remainCount -= item.maxCount;
                        }
                        else
                        {
                            if (items.Count >= maxCount)
                                return false;
                            items.Add(item.Instantiate(remainCount) as Item);
                            remainCount = 0;
                        }
                    }
                }
                else
                {
                    if (items.Count >= maxCount)
                        return false;
                    items.Add(item.Clone() as Item);
                }
            }
            if (OnItemChange != null)
                OnItemChange();
            return true;
        }
        public bool Consume(Item item)
        {
            if (items.Where(x => x.id == item.id).Sum(x => x.itemCount) < item.itemCount)
                return false;
            if(items.Any(x => x.id == item.id))
            {
                Item targetItem = items.First(x => x.id == item.id);
                if (item.itemCount > targetItem.itemCount)
                {
                    int remainCount = item.itemCount - targetItem.itemCount;
                    items.Remove(targetItem);
                    while(remainCount > 0)
                    {
                        targetItem = items.First(x => x.id == item.id);
                        if (remainCount > targetItem.itemCount)
                        {
                            items.Remove(targetItem);
                            remainCount -= targetItem.itemCount;
                        }
                        else
                        {
                            targetItem.Decrease(remainCount);
                            if(targetItem.itemCount == 0)
                                items.Remove(targetItem);
                            remainCount = 0;
                        }
                    }
                }
                else
                {
                    targetItem.Decrease(item.itemCount);
                    if(targetItem.itemCount == 0)
                    {
                        items.Remove(targetItem);
                    }
                }
            }
            if (OnItemChange != null)
                OnItemChange();
            return true;
        }
    }
}
