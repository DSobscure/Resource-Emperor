using REProtocol;

namespace REStructure.Items
{
    public abstract class Product : Item
    {
        protected Product() : base() { }
        protected Product(int itemCount) : base(itemCount)
        {

        }
    }
}
