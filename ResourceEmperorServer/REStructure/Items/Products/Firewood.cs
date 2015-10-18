﻿using REProtocol;

namespace REStructure.Items.Products
{
    public class Firewood : Product
    {
        static ItemID _id;
        static string _name;
        static string _description;

        static Firewood()
        {
            _id = ItemID.Firewood;
            _name = "柴薪";
            _description = "";
        }
        protected Firewood() { }
        public Firewood(int itemCount) : base(itemCount) { }

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

        public override object Clone()
        {
            return new Firewood(itemCount);
        }
    }
}
