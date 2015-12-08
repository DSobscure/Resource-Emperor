using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using REProtocol;

namespace REStructure
{
    public class Market
    {
        public List<Commodity> catalog { get; protected set; }
        public event Action OnCommodityChange;

        public Market(List<Commodity> catalog)
        {
            this.catalog = catalog;
        }

        public bool Purchase(int commodityIndex, int count, Player seller, Inventory inventory)
        {
            if (catalog[commodityIndex].Purchase(count, seller, inventory))
            {
                if (OnCommodityChange != null)
                    OnCommodityChange();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Sell(int commodityIndex, int count, Player seller, Inventory inventory)
        {
            if (catalog[commodityIndex].Sell(count, seller, inventory))
            {
                if (OnCommodityChange != null)
                    OnCommodityChange();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
