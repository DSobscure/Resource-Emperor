﻿using System;
using REProtocol;

namespace REStructure.Items.Materials
{
    public class IronOre : MineralMaterial
    {
        static ItemID _id;
        static string _name;
        static string _description;
        static MineralType _type;

        static IronOre()
        {
            _id = ItemID.IronOre;
            _name = "鐵礦";
            _description = "";
            _type = MineralType.Mine;
        }
        protected IronOre() : base() { }
        public IronOre(int itemCount) : base(itemCount) { }

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

        public override MineralType type
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
            return new IronOre(itemCount);
        }
    }
}
