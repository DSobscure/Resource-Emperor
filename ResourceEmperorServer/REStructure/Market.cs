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

        public bool Purchase(ItemID commodityID, int count, Player seller, Inventory inventory)
        {
            if (catalog.Find(x=>x.item.id == commodityID).Purchase(count, seller, inventory))
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

        public bool Sell(ItemID commodityID, int count, Player seller, Inventory inventory)
        {
            if (catalog.Find(x => x.item.id == commodityID).Sell(count, seller, inventory))
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

        public void Update(Market markey)
        {
            catalog = markey.catalog;
            if (OnCommodityChange != null)
                OnCommodityChange();
        }
    }
}
