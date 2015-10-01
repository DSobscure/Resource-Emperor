using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using REProtocol;

namespace REStructure.Items.Products
{
    public class Iron : MetalProduct
    {
        public Iron() : base() { }
        public Iron(int itemCount) : base(itemCount)
        {
            id = ItemID.Iron;
            name = "鐵塊";
            description = "";
        }
    }
}
