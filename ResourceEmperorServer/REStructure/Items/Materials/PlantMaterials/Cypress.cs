﻿using REProtocol;

namespace REStructure.Items.Materials
{
    public class Cypress : PlantMaterial
    {
        static ItemID _id;
        static string _name;
        static string _description;
        static PlantType _type;

        static Cypress()
        {
            _id = ItemID.Cypress;
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
            return new Cypress(itemCount);
        }
    }
}
