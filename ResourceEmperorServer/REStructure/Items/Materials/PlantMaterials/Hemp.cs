using REProtocol;

namespace REStructure.Items.Materials
{
    public class Hemp : PlantMaterial
    {
        static ItemID _id;
        static string _name;
        static string _description;
        static PlantType _type;

        static Hemp()
        {
            _id = ItemID.Hemp;
            _name = "麻草";
            _description = "";
            _type = PlantType.Herb;
        }
        protected Hemp() { }
        public Hemp(int itemCount) : base(itemCount) { }

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
            return new Hemp(itemCount);
        }
    }
}
