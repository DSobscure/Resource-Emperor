using System;
using System.Collections.Generic;
using REProtocol;
using REStructure.Items.Materials;
using REStructure.Items.Products;

namespace REStructure.Appliances
{
    public class WorkTable : Appliance
    {
        static ApplianceID _id;
        static string _name;
        static Dictionary<ProduceMethodID, ProduceMethod> _methods;

        static WorkTable()
        {
            _id = ApplianceID.WorkTable;
            _name = "工作桌";
            _methods = new Dictionary<ProduceMethodID, ProduceMethod>()
            {
                { ProduceMethodID.WorkTable__Log__WoodenAxle, new ProduceMethod(ProduceMethodID.WorkTable__Log__WoodenAxle, "製作木製輪軸", new Item[]{ new Log(1) }, new Object[]{ new WoodenAxle(1) }, 60) },
                { ProduceMethodID.WorkTable__Timber_WorkTable__SawPlatform, new ProduceMethod(ProduceMethodID.WorkTable__Timber_WorkTable__SawPlatform, "升級成鋸台", new Item[] { new Timber(10) }, new Object[] { new SawPlatform() }, 300) }
            };
        }
        public WorkTable() { }

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
