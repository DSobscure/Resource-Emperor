using System;
using REProtocol;

namespace REStructure.Items
{
    public abstract class Material : Item
    {
        static int _maxCount;

        static Material()
        {
            _maxCount = 100;
        }
        protected Material() : base() { }
        protected Material(int itemCount) : base(itemCount)
        {

        }

        public override int maxCount
        {
            get
            {
                return _maxCount;
            }
        }
    }
}
