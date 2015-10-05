using System;
using System.Collections.Generic;
using REProtocol;
using REStructure.Items.Materials;
using REStructure.Items.Products;

namespace REStructure.Appliances
{
    public class Loom : Appliance
    {
        static ApplianceID _id;
        static string _name;
        static Dictionary<ProduceMethodID, ProduceMethod> _methods;

        static Loom()
        {
            _id = ApplianceID.Loom;
            _name = "織布機";
            _methods = new Dictionary<ProduceMethodID, ProduceMethod>()
            {
                { ProduceMethodID.Loom__Cotton__Cottonwool, new ProduceMethod(ProduceMethodID.Loom__Cotton__Cottonwool, "製作棉絮", new Item[]{ new Cotton(1) }, new Object[]{ new Cottonwool(1) }, 30) },
                { ProduceMethodID.Loom__Cottonwool__CottonThread, new ProduceMethod(ProduceMethodID.Loom__Cottonwool__CottonThread, "製作棉線", new Item[] { new Cottonwool(1) }, new Object[] { new CottonThread(1) }, 60) },
                { ProduceMethodID.Loom__CottonThread__CottonRope, new ProduceMethod(ProduceMethodID.Loom__CottonThread__CottonRope, "製作棉繩", new Item[] { new CottonThread(3) }, new Object[] { new CottonRope(1) }, 60) },
                { ProduceMethodID.Loom__CottonThread__CottonCloth, new ProduceMethod(ProduceMethodID.Loom__CottonThread__CottonCloth, "製作棉布", new Item[] { new CottonThread(10) }, new Object[] { new CottonCloth(1) }, 300) }
            };
        }
        public Loom() { }

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
