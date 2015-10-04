using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using REProtocol;
using REStructure.Items.Materials;
using REStructure.Items.Products;

namespace REStructure
{
    sealed public class ProduceMethodDictionary : IDictionary<ProduceMethodID, ProduceMethod>
    {
        private Dictionary<ProduceMethodID, ProduceMethod> produceMethods;

        public ProduceMethodDictionary()
        {
            produceMethods = new Dictionary<ProduceMethodID, ProduceMethod>();
            produceMethods.Add(ProduceMethodID.Iron, new ProduceMethod(new Item[] { new IronOre(1), new Coal(1), new Log(1), new Bamboo(1), new Water(1) },new Item[] { new IronBlock(1) },10));
        }

        public ProduceMethod this[ProduceMethodID key]
        {
            get
            {
                return produceMethods[key];
            }

            set
            {
                produceMethods[key] = value;
            }
        }

        public int Count
        {
            get
            {
                return produceMethods.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return true;
            }
        }

        public ICollection<ProduceMethodID> Keys
        {
            get
            {
                return produceMethods.Keys;
            }
        }

        public ICollection<ProduceMethod> Values
        {
            get
            {
                return produceMethods.Values;
            }
        }

        public void Add(KeyValuePair<ProduceMethodID, ProduceMethod> item)
        {
            produceMethods.Add(item.Key, item.Value);
        }

        public void Add(ProduceMethodID key, ProduceMethod value)
        {
            produceMethods.Add(key, value);
        }

        public void Clear()
        {
            produceMethods.Clear();
        }

        public bool Contains(KeyValuePair<ProduceMethodID, ProduceMethod> item)
        {
            return produceMethods.ContainsKey(item.Key) && produceMethods[item.Key] == item.Value;
        }

        public bool ContainsKey(ProduceMethodID key)
        {
            return produceMethods.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<ProduceMethodID, ProduceMethod>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<ProduceMethodID, ProduceMethod>> GetEnumerator()
        {
            return produceMethods.GetEnumerator();
        }

        public bool Remove(KeyValuePair<ProduceMethodID, ProduceMethod> item)
        {
            return this.Contains(item) && produceMethods.Remove(item.Key);
        }

        public bool Remove(ProduceMethodID key)
        {
            return produceMethods.Remove(key);
        }

        public bool TryGetValue(ProduceMethodID key, out ProduceMethod value)
        {
            return produceMethods.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return produceMethods.GetEnumerator();
        }
    }
}
