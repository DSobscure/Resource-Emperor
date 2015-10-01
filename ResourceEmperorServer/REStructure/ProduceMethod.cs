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
        public Item[] products;
        public int processTime;

        public ProduceMethod(Item[] materials, Item[] products, int processTime)
        {
            this.materials = materials;
            this.products = products;
            this.processTime = processTime;
        }

        public Item[] Process(Item[] materials)
        {
            foreach(var item in this.materials)
            {
                if (!materials.Contains(item))
                    return null;
            }
            return (Item[])products.Clone();
        }
    }
}
