using System;
using REProtocol;

namespace REStructure.Items.Materials
{
    public class Brine : LiquidMaterial
    {
        static ItemID _id;
        static string _name;
        static string _description;
        static LiquidType _type;

        static Brine()
        {
            _id = ItemID.Brine;
            _name = "海水";
            _description = "海水敘述";
            _type = LiquidType.Water;
        }
        protected Brine() { }
        public Brine(int itemCount) : base(itemCount) { }

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

        public override LiquidType type
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
            return new Brine(itemCount);
        }
    }
}
