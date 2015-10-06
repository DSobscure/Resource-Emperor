using System;
using System.Collections.Generic;
using REProtocol;
using REStructure.Items.Materials;
using REStructure.Items.Products;

namespace REStructure.Appliances
{
    public class HTKiln : Appliance
    {
        static ApplianceID _id;
        static string _name;
        static Dictionary<ProduceMethodID, ProduceMethod> _methods;

        static HTKiln()
        {
            _id = ApplianceID.HTKiln;
            _name = "高溫窯";
            _methods = new Dictionary<ProduceMethodID, ProduceMethod>()
            {
                { ProduceMethodID.StoneStove__Log_Firewood__Charcoal, new ProduceMethod(ProduceMethodID.StoneStove__Log_Firewood__Charcoal, "製作木炭", new Item[]{ new Log(1), new Firewood(1) }, new Object[]{ new Charcoal(3) }, 10) },
                { ProduceMethodID.StoneStove__Clay_Firewood__Brick, new ProduceMethod(ProduceMethodID.StoneStove__Clay_Firewood__Brick, "製作磚塊", new Item[] { new Clay(1), new Firewood(1) }, new Object[] { new Brick(1) }, 10) },

                { ProduceMethodID.LTKiln__RawRubber_Firewood__Rubber, new ProduceMethod(ProduceMethodID.LTKiln__RawRubber_Firewood__Rubber, "製作橡膠", new Item[]{ new RawRubber(1), new Firewood(1) }, new Object[]{ new Rubber(1) }, 30) },

                { ProduceMethodID.HTKiln__IronOre_Coal__IronBlock, new ProduceMethod(ProduceMethodID.HTKiln__IronOre_Coal__IronBlock, "製作鐵塊", new Item[]{ new IronOre(1), new Coal(1) }, new Object[]{ new IronBlock(1) }, 60) },
                { ProduceMethodID.HTKiln__IronOre_Charcoal__IronBlock, new ProduceMethod(ProduceMethodID.HTKiln__IronOre_Charcoal__IronBlock, "製作鐵塊", new Item[] { new IronOre(1), new Charcoal(2) }, new Object[] { new IronBlock(1) }, 60) },
                { ProduceMethodID.HTKiln__CopperOre_Coal__CopperBlock, new ProduceMethod(ProduceMethodID.HTKiln__CopperOre_Coal__CopperBlock, "製作銅塊", new Item[] { new CopperOre(1), new Coal(1) }, new Object[] { new CopperBlock(1) } , 60) },
                { ProduceMethodID.HTKiln__CopperOre_Charcoal__CopperBlock, new ProduceMethod(ProduceMethodID.HTKiln__CopperOre_Charcoal__CopperBlock, "製作銅塊", new Item[] { new CopperOre(1), new Charcoal(2) }, new Object[] { new CopperBlock(1) } , 60) },
                { ProduceMethodID.HTKiln__Clay_IronBlock_Coal__Cupola, new ProduceMethod(ProduceMethodID.HTKiln__Clay_IronBlock_Coal__Cupola, "製作熔鐵爐", new Item[] { new Clay(10), new IronBlock(10), new Coal(6) }, new Object[] { new Cupola() } , 600) },
                { ProduceMethodID.HTKiln__IronBlock_Brick_Timber__BlacksmithPlatform, new ProduceMethod(ProduceMethodID.HTKiln__IronBlock_Brick_Timber__BlacksmithPlatform, "製作鍛造平台", new Item[] { new IronBlock(10), new Brick(8), new Timber(4) }, new Object[] { new BlacksmithPlatform() } , 600) },
                { ProduceMethodID.HTKiln__WroughIron_Copper_Timber_Clay__SteelMold, new ProduceMethod(ProduceMethodID.HTKiln__WroughIron_Copper_Timber_Clay__SteelMold, "製作鋼鐵鑄模", new Item[] { new WroughIron(8), new Copper(5), new Timber(10), new Clay(8) }, new Object[] { new SteelMold() } , 900) }
            };
        }
        public HTKiln() { }

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
