using System;
using System.Collections.Generic;
using REProtocol;
using REStructure.Items.Products;

namespace REStructure.Appliances
{
    public class BlacksmithPlatform : Appliance
    {
        static ApplianceID _id;
        static string _name;
        static Dictionary<ProduceMethodID, ProduceMethod> _methods;

        static BlacksmithPlatform()
        {
            _id = ApplianceID.BlacksmithPlatform;
            _name = "鍛造平台";
            _methods = new Dictionary<ProduceMethodID, ProduceMethod>()
            {
                { ProduceMethodID.BlacksmithPlatform__Copper__CopperSheet, new ProduceMethod(ProduceMethodID.BlacksmithPlatform__Copper__CopperSheet, "製作銅片", new Item[]{ new Copper(1) }, new Object[]{ new CopperSheet(1) }, 60) },
                { ProduceMethodID.BlacksmithPlatform__WroughIron__IronSheet, new ProduceMethod(ProduceMethodID.BlacksmithPlatform__WroughIron__IronSheet, "製作鐵皮", new Item[] { new WroughIron(1) }, new Object[] { new IronSheet(1) },60) },
                { ProduceMethodID.BlacksmithPlatform__WroughIron__Blade, new ProduceMethod(ProduceMethodID.BlacksmithPlatform__WroughIron__Blade, "製作刀片", new Item[] { new WroughIron(1) }, new Object[] { new Blade(1) } ,60) }
            };
        }
        public BlacksmithPlatform() { }

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
