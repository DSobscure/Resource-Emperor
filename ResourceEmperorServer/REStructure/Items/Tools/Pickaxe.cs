using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace REStructure.Items.Tools
{
    public abstract class Pickaxe : Tool
    {
        public Pickaxe() { }
        public Pickaxe(int itemCount, int durability, int durabilityLimit) : base(itemCount, durability, durabilityLimit) { }
    }
}
