using REProtocol;

namespace REStructure.Items
{
    public abstract class Product : Item
    {
        static int _maxCount;

        static Product()
        {
            _maxCount = 10;
        }

        protected Product() : base() { }
        protected Product(int itemCount) : base(itemCount)
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
