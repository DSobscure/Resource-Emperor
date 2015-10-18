using System;
using REProtocol;

namespace REStructure.Items.Materials
{
    public class RawRubber : PlantMaterial
    {
        static ItemID _id;
        static string _name;
        static string _description;
        static PlantType _type;

        static RawRubber()
        {
            _id = ItemID.Rubber;
            _name = "生橡膠";
            _description = "";
            _type = PlantType.Product;
        }
        protected RawRubber() { }
        public RawRubber(int itemCount) : base(itemCount) { }

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
            return new RawRubber(itemCount);
        }
    }
}
