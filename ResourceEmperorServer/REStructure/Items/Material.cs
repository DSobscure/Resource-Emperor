﻿using REProtocol;

namespace REStructure.Items
{
    public abstract class Material : Item
    {
        protected Material() : base() { }
        protected Material(int itemCount) : base(itemCount)
        {

        }
    }
}
