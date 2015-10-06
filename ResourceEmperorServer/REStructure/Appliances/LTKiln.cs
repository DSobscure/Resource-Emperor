using System;
using System.Collections.Generic;
using REProtocol;
using REStructure.Items.Materials;
using REStructure.Items.Products;

namespace REStructure.Appliances
{
    public class LTKiln : Appliance , IUpgradable
    {
        static ApplianceID _id;
        static string _name;
        static Dictionary<ProduceMethodID, ProduceMethod> _methods;

        static LTKiln()
        {
            _id = ApplianceID.LTKiln;
            _name = "低溫窯";
            _methods = new Dictionary<ProduceMethodID, ProduceMethod>()
            {
                { ProduceMethodID.StoneStove__Log_Firewood__Charcoal, new ProduceMethod(ProduceMethodID.StoneStove__Log_Firewood__Charcoal, "製作木炭", new Item[]{ new Log(1), new Firewood(1) }, new Object[]{ new Charcoal(3) }, 30) },
                { ProduceMethodID.StoneStove__Clay_Firewood__Brick, new ProduceMethod(ProduceMethodID.StoneStove__Clay_Firewood__Brick, "製作磚塊", new Item[] { new Clay(1), new Firewood(1) }, new Object[] { new Brick(1) }, 30) },

                { ProduceMethodID.LTKiln__RawRubber_Firewood__Rubber, new ProduceMethod(ProduceMethodID.LTKiln__RawRubber_Firewood__Rubber, "製作橡膠", new Item[]{ new RawRubber(1), new Firewood(1) }, new Object[]{ new Rubber(1) }, 60) },
                { ProduceMethodID.LTKiln__Clay_LTKiln__HTKiln, new ProduceMethod(ProduceMethodID.LTKiln__Clay_LTKiln__HTKiln, "升級成高溫窯", new Item[] { new Clay(10) }, new Object[] { new HTKiln() }, 600) }
            };
        }
        public LTKiln() { }

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

        public bool UpgradeCheck(object target)
        {
            return target is HTKiln;
        }

        public object Upgrade()
        {
            return new HTKiln();
        }
    }
}
