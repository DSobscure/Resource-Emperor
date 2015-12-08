using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using REProtocol;
using Newtonsoft.Json;

namespace REStructure
{
    public class ProduceMethod
    {
        [JsonProperty("id")]
        public ProduceMethodID id;
        [JsonProperty("materials")]
        public Item[] materials;
        [JsonProperty("products")]
        public object[] products;
        [JsonProperty("processTime")]
        public int processTime;
        [JsonProperty("title")]
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
            int newItemCount = 0;
            foreach(object product in products)
            {
                Item item = product as Item;
                if (item is Item && !inventory.Any(x=>x.id == item.id))
                    newItemCount++;
            }
            if (newItemCount + inventory.Count > inventory.maxCount)
                return false;
            foreach (var item in this.materials)
            {
                if (!inventory.Any(x=>x.id == item.id) || inventory.Where(x=>x.id == item.id).Sum(x=>x.itemCount) < item.itemCount)
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
                    inventory.Consume(item);
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
