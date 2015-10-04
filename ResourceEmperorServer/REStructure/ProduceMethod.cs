using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using REProtocol;

namespace REStructure
{
    public class ProduceMethod
    {
        public ProduceMethodID id;
        public Item[] materials;
        public Object[] products;
        public int processTime;
        public string title;

        public ProduceMethod(string title, Item[] materials, Object[] products, int processTime)
        {
            this.title = title;
            this.materials = materials;
            this.products = products;
            this.processTime = processTime;
        }

        public Object[] Process(Dictionary<ItemID,Item> materials)
        {
            foreach(var item in this.materials)
            {
                if (!materials.ContainsKey(item.id) || materials[item.id].itemCount < item.itemCount)
                {
                    return null;
                }
            }
            return (Object[])products.Clone();
        }
    }
}
