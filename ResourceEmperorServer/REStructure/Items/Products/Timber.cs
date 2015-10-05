using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using REProtocol;

namespace REStructure.Items.Products
{
    public class Timber : Product
    {
        static ItemID _id;
        static string _name;
        static string _description;

        static Timber()
        {
            _id = ItemID.Timber;
            _name = "木材";
            _description = "";
        }
        protected Timber() { }
        public Timber(int itemCount) : base(itemCount) { }

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

        public override object Clone()
        {
            return new Timber(itemCount);
        }
    }
}
