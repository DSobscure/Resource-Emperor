using System;
using System.Collections.Generic;
using REProtocol;
using REStructure.Items.Materials;
using REStructure.Items.Products;

namespace REStructure.Appliances
{
    public class StoneStove : Appliance , IUpgradable
    {
        static ApplianceID _id;
        static string _name;
        static Dictionary<ProduceMethodID, ProduceMethod> _methods;

        static StoneStove()
        {
            _id = ApplianceID.StoneStove;
            _name = "石灶";
            _methods = new Dictionary<ProduceMethodID, ProduceMethod>()
            {
                { ProduceMethodID.StoneStove__Log_Firewood__Charcoal, new ProduceMethod(ProduceMethodID.StoneStove__Log_Firewood__Charcoal, "製作木炭", new Item[]{ new Log(1), new Firewood(1) }, new Object[]{ new Charcoal(3) }, 60) },
                { ProduceMethodID.StoneStove__Clay_Firewood__Brick, new ProduceMethod(ProduceMethodID.StoneStove__Clay_Firewood__Brick, "製作磚塊", new Item[] { new Clay(1), new Firewood(1) }, new Object[] { new Brick(1) }, 60) },
                { ProduceMethodID.StoneStove__Clay_StoneStove__LTKiln, new ProduceMethod(ProduceMethodID.StoneStove__Clay_StoneStove__LTKiln, "升級成低溫窯", new Item[] { new Clay(10) }, new Object[] { new LTKiln() } , 300) }
            };
        }
        public StoneStove() { }

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
            return target is LTKiln;
        }

        public object Upgrade()
        {
            return new LTKiln();
        }
    }
}
