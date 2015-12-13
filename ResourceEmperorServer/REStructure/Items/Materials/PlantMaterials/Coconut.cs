using REProtocol;

namespace REStructure.Items.Materials
{
    public class Coconut : PlantMaterial
    {
        static ItemID _id;
        static string _name;
        static string _description;
        static PlantType _type;

        static Coconut()
        {
            _id = ItemID.Coconut;
            _name = "椰子";
            _description = "";
            _type = PlantType.Product;
        }
        protected Coconut() { }
        public Coconut(int itemCount) : base(itemCount) { }

        public override ItemID id
        {
            get
            {
                return _id;
            }
        }

        public override string name
        {
            get
            {
                return _name;
            }
        }

        public override string description
        {
            get
            {
                return _description;
            }
        }

        public override PlantType type
        {
            get
            {
                return _type;
            }

            protected set
            {
                _type = value;
            }
        }

        public override object Clone()
        {
            return new Coconut(itemCount);
        }
    }
}
