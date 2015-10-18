using REProtocol;

namespace REStructure.Items.Materials
{
    public class Oak : PlantMaterial
    {
        static ItemID _id;
        static string _name;
        static string _description;
        static PlantType _type;

        static Oak()
        {
            _id = ItemID.Oak;
            _name = "橡木";
            _description = "";
            _type = PlantType.Wood;
        }
        protected Oak() { }
        public Oak(int itemCount) : base(itemCount) { }

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
            return new Oak(itemCount);
        }
    }
}
