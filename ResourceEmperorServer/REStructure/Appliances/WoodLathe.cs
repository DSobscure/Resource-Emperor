using System;
using System.Collections.Generic;
using REProtocol;
using REStructure.Items.Materials;
using REStructure.Items.Products;

namespace REStructure.Appliances
{
    public class WoodLathe : Appliance
    {
        static ApplianceID _id;
        static string _name;
        static Dictionary<ProduceMethodID, ProduceMethod> _methods;

        static WoodLathe()
        {
            _id = ApplianceID.WoodLathe;
            _name = "木工車床";
            _methods = new Dictionary<ProduceMethodID, ProduceMethod>()
            {
                { ProduceMethodID.WoodLathe__CypressTimber_WoodenAxle__Loom, new ProduceMethod(ProduceMethodID.WoodLathe__CypressTimber_WoodenAxle__Loom, "製作織布機", new Item[]{ new CypressTimber(10), new WoodenAxle(3) }, new Object[]{ new Loom() }, 600) },
                { ProduceMethodID.WoodLathe__CypressTimber_WoodenAxle__PaperMachine, new ProduceMethod(ProduceMethodID.WoodLathe__CypressTimber_WoodenAxle__PaperMachine, "製作造紙機", new Item[] { new CypressTimber(8), new WoodenAxle(2) }, new Object[] { new PaperMachine() }, 480) }
            };
        }
        public WoodLathe() { }

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
