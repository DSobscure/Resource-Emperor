using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace REStructure.Items.Tools
{
    public abstract class Axe : Tool
    {
        public Axe() { }
        public Axe(int itemCount, int durability, int durabilityLimit) : base(itemCount, durability, durabilityLimit) { }
    }
}
