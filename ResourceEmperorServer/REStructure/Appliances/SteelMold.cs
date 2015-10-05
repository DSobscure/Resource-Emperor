using System;
using System.Collections.Generic;
using REProtocol;
using REStructure.Items.Materials;
using REStructure.Items.Products;

namespace REStructure.Appliances
{
    public class SteelMold : Appliance
    {
        static ApplianceID _id;
        static string _name;
        static Dictionary<ProduceMethodID, ProduceMethod> _methods;

        static SteelMold()
        {
            _id = ApplianceID.SteelMold;
            _name = "鋼鐵鑄模";
            _methods = new Dictionary<ProduceMethodID, ProduceMethod>()
            {
                { ProduceMethodID.SteelMold__WroughIron__CircularSawBlade, new ProduceMethod(ProduceMethodID.SteelMold__WroughIron__CircularSawBlade, "製作圓鋸鋸片", new Item[]{ new WroughIron(1) }, new Object[]{ new CircularSawBlade(1) }, 120) },
                { ProduceMethodID.SteelMold__WroughIron__MetalGear, new ProduceMethod(ProduceMethodID.SteelMold__WroughIron__MetalGear, "製作金屬齒輪", new Item[] { new WroughIron(1) }, new Object[] { new MetalGear(4) }, 120) },
                { ProduceMethodID.SteelMold__WroughIron__Rivet, new ProduceMethod(ProduceMethodID.SteelMold__WroughIron__Rivet, "製作鉚釘", new Item[] { new WroughIron(1) }, new Object[] { new Rivet(6) } , 120) },
                { ProduceMethodID.SteelMold__WroughIron__IronBar, new ProduceMethod(ProduceMethodID.SteelMold__WroughIron__IronBar, "製作鐵條", new Item[] { new WroughIron(1) }, new Object[] { new IronBar(1) } , 120) },
                { ProduceMethodID.SteelMold__WroughIron__IronPipe, new ProduceMethod(ProduceMethodID.SteelMold__WroughIron__IronPipe, "製作鐵管", new Item[] { new WroughIron(1) }, new Object[] { new IronPipe(1) } , 120) }
            };
        }
        public SteelMold() { }

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
