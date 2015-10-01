using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace REStructure.Items.Products
{
    public abstract class MetalProduct : Product
    {
        public MetalProduct() : base() { }
        public MetalProduct(int itemCount) : base(itemCount)
        {

        }
    }
}
