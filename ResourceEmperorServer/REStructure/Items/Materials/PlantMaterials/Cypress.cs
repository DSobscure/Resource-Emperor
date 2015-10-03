using REProtocol;

namespace REStructure.Items.Materials.PlantMaterials
{
    public class Cypress : PlantMaterial
    {
        static ItemID _id;
        static string _name;
        static string _description;
        static PlantType _type;

        static Cypress()
        {
            _id = ItemID.Log;
            _name = "檜木";
            _description = "";
            _type = PlantType.Wood;
        }
        protected Cypress() { }
        public Cypress(int itemCount) : base(itemCount) { }

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
    }
}
