using System;
using System.Collections.Generic;
using REProtocol;
using REStructure.Items.Materials;
using REStructure.Items.Products;

namespace REStructure.Appliances
{
    public class PaperMachine : Appliance
    {
        static ApplianceID _id;
        static string _name;
        static Dictionary<ProduceMethodID, ProduceMethod> _methods;

        static PaperMachine()
        {
            _id = ApplianceID.PaperMachine;
            _name = "造紙機";
            _methods = new Dictionary<ProduceMethodID, ProduceMethod>()
            {
                { ProduceMethodID.PaperMachine__Log_Water__Paper, new ProduceMethod(ProduceMethodID.PaperMachine__Log_Water__Paper, "製作紙", new Item[]{ new Log(1), new Water(1) }, new Object[]{ new Paper(1) }, 60) },
                { ProduceMethodID.PaperMachine__Bamboo_Water__Paper, new ProduceMethod(ProduceMethodID.PaperMachine__Bamboo_Water__Paper, "製作紙", new Item[] { new Bamboo(2), new Water(1) }, new Object[] { new Paper(1) }, 60) }
            };
        }
        public PaperMachine() { }

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
