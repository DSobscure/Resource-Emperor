using System;
using System.Collections.Generic;
using REProtocol;
using REStructure.Items.Materials;
using REStructure.Items.Products;

namespace REStructure.Appliances
{
    public class Agitator : Appliance
    {
        static ApplianceID _id;
        static string _name;
        static Dictionary<ProduceMethodID, ProduceMethod> _methods;

        static Agitator()
        {
            _id = ApplianceID.Agitator;
            _name = "攪拌器";
            _methods = new Dictionary<ProduceMethodID, ProduceMethod>()
            {
                { ProduceMethodID.Agitator__Clay_Rock__StoneStove, new ProduceMethod("製作石灶", new Item[]{ new Clay(5), new Rock(5) }, new Object[]{ new StoneStove() }, 180) }
            };
        }
        public Agitator() { }

        public override ApplianceID id
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

        public override Dictionary<ProduceMethodID, ProduceMethod> methods
        {
            get
            {
                return _methods;
            }

            protected set
            {
                _methods = value;
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
    }
}
