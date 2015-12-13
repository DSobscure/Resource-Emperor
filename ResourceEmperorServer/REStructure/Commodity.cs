using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace REStructure
{
    public class Commodity
    {
        public Item item { get; protected set; }
        public int stock { get; protected set; }
        public int standardStock { get; protected set; }
        public int maxStock { get; protected set; }
        public int basicPrice { get; protected set; }
        public int price
        {
            get
            {
                return (int)Math.Round(basicPrice*Math.Pow(1.0-((double)stock -standardStock)/maxStock,priceLevel));
            }
        }
        public double priceLevel { get; protected set; }

        public Commodity(Item item, int stock, int standardStock , int maxStock, int basicPrice, double priceLevel)
        {
            this.item = item;
            this.standardStock = standardStock;
            this.stock = stock;
            this.maxStock = maxStock;
            this.basicPrice = basicPrice;
            this.priceLevel = priceLevel;
        }

        public bool Purchase(int count, Player customer, Inventory inventory)
        {
            lock(this)
            {
                int cost = price * count;
                if (stock >=count && customer.SpendMoney(cost))
                {
                    stock -= count;
                    return inventory.Stack(item.Instantiate(count) as Item);
                }
                else
                {
                    return false;
                }
            }
        }
        public bool Sell(int count, Player seller ,Inventory inventory)
        {
            lock(this)
            {
                if (inventory.Sum(x=>(x.id == item.id)?x.itemCount:0) >= count && stock + count <= maxStock)
                {
                    inventory.Consume(item.Instantiate(count) as Item);
                    stock += count;
                    seller.GetMoney(price * count);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
