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
        public object[] products;
        public int processTime;
        public string title;

        public ProduceMethod(ProduceMethodID id, string title, Item[] materials, object[] products, int processTime)
        {
            this.id = id;
            this.title = title;
            this.materials = materials;
            this.products = products;
            this.processTime = processTime;
        }

        public bool Sufficient(Inventory inventory)
        {
            foreach (var item in this.materials)
            {
                if (!inventory.ContainsKey(item.id) || inventory[item.id].itemCount < item.itemCount)
                {
                    return false;
                }
            }
            return true;
        }

        public bool Process(Inventory inventory, out object[] products)
        {
            if(Sufficient(inventory))
            {
                foreach (var item in this.materials)
                {
                    inventory[item.id].Decrease(item.itemCount);
                    if(inventory[item.id].itemCount == 0)
                    {
                        inventory.Remove(item.id);
                    }
                }
                products = this.products;
                return true;
            }
            else
            {
                products = null;
                return false;
            }
        }
    }
}
