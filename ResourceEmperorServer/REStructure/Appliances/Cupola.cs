using System;
using System.Collections.Generic;
using REProtocol;
using REStructure.Items.Materials;
using REStructure.Items.Products;

namespace REStructure.Appliances
{
    public class Cupola : Appliance
    {
        static ApplianceID _id;
        static string _name;
        static Dictionary<ProduceMethodID, ProduceMethod> _methods;

        static Cupola()
        {
            _id = ApplianceID.Cupola;
            _name = "熔鐵爐";
            _methods = new Dictionary<ProduceMethodID, ProduceMethod>()
            {
                { ProduceMethodID.Cupola__IronOre_Coal__IronBlock, new ProduceMethod(ProduceMethodID.Cupola__IronOre_Coal__IronBlock, "製作鐵塊", new Item[]{ new IronOre(2), new Coal(1) }, new Object[]{ new IronBlock(2) }, 60) },
                { ProduceMethodID.Cupola__IronOre_Charcoal__IronBlock, new ProduceMethod(ProduceMethodID.Cupola__IronOre_Charcoal__IronBlock, "製作鐵塊", new Item[] { new IronOre(2), new Charcoal(2) }, new Object[] { new IronBlock(2) }, 60) },
                { ProduceMethodID.Cupola__CopperOre_Coal__CopperBlock, new ProduceMethod(ProduceMethodID.Cupola__CopperOre_Coal__CopperBlock, "製作銅塊", new Item[] { new CopperOre(2), new Coal(1) }, new Object[] { new CopperBlock(2) } , 60) },
                { ProduceMethodID.Cupola__CopperOre_Charcoal__CopperBlock, new ProduceMethod(ProduceMethodID.Cupola__CopperOre_Charcoal__CopperBlock, "製作銅塊", new Item[] { new CopperOre(2), new Charcoal(2) }, new Object[] { new CopperBlock(2) } , 60) },
                { ProduceMethodID.Cupola__IronBlock_Coal__WroughIron, new ProduceMethod(ProduceMethodID.Cupola__IronBlock_Coal__WroughIron, "製作熟鐵材", new Item[] { new IronBlock(2), new Coal(1) }, new Object[] { new WroughIron(1) } , 120) },
                { ProduceMethodID.Cupola__IronBlock_Charcoal__WroughIron, new ProduceMethod(ProduceMethodID.Cupola__IronBlock_Charcoal__WroughIron, "製作熟鐵材", new Item[] { new IronBlock(2), new Charcoal(2) }, new Object[] { new WroughIron(1) } , 120) },
                { ProduceMethodID.Cupola__CopperBlock_Coal__Copper, new ProduceMethod(ProduceMethodID.Cupola__CopperBlock_Coal__Copper, "製作銅材", new Item[] { new CopperBlock(2), new Coal(1) }, new Object[] { new Copper(1) } , 120) },
                { ProduceMethodID.Cupola__CopperBlock_Charcoal__Copper, new ProduceMethod(ProduceMethodID.Cupola__CopperBlock_Charcoal__Copper, "製作銅材", new Item[] { new CopperBlock(2), new Charcoal(2) }, new Object[] { new Copper(1) } , 120) }
            };
        }
        public Cupola() { }

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
