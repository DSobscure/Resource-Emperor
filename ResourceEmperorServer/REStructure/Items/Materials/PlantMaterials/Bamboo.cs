using REProtocol;

namespace REStructure.Items.Materials
{
    public class Bamboo : PlantMaterial
    {
        static ItemID _id;
        static string _name;
        static string _description;
        static PlantType _type;

        static Bamboo()
        {
            _id = ItemID.Bamboo;
            _name = "竹子";
            _description = "";
            _type = PlantType.Wood;
        }
        protected Bamboo() { }
        public Bamboo(int itemCount) : base(itemCount) { }

        public override ItemID id
        {
            get
            {
                return _id;
            }

            protected set
            {
                _id = value;
            }
        }

        public override string name
        {
            get
            {
                return _name;
            }

            protected set
            {
                _name = value;
            }
        }

        public override string description
        {
            get
            {
                return _description;
            }

            protected set
            {
                _description = value;
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
            return new Bamboo(itemCount);
        }
    }
}
