using System;
using System.Collections.Generic;
using REProtocol;
using REStructure.Items.Materials;
using REStructure.Items.Products;

namespace REStructure.Appliances
{
    public class SawPlatform : Appliance
    {
        static ApplianceID _id;
        static string _name;
        static Dictionary<ProduceMethodID, ProduceMethod> _methods;

        static SawPlatform()
        {
            _id = ApplianceID.SawPlatform;
            _name = "鋸台";
            _methods = new Dictionary<ProduceMethodID, ProduceMethod>()
            {
                { ProduceMethodID.SawPlatform__Oak__OakTimber, new ProduceMethod(ProduceMethodID.SawPlatform__Oak__OakTimber, "製作橡木材", new Item[]{ new Oak(1) }, new Object[]{ new OakTimber(1) }, 0) },
                { ProduceMethodID.SawPlatform__Cypress__CypressTimber, new ProduceMethod(ProduceMethodID.SawPlatform__Cypress__CypressTimber, "製作檜木材", new Item[] { new Cypress(1) }, new Object[] { new CypressTimber(1) }, 0) },
                { ProduceMethodID.SawPlatform__OakTimber__Barrel, new ProduceMethod(ProduceMethodID.SawPlatform__OakTimber__Barrel, "製作木桶", new Item[] { new OakTimber(5) }, new Object[] { new Barrel(1) } , 60) },
                { ProduceMethodID.SawPlatform__Barrel_Timber_WoodenAxle__Agitator, new ProduceMethod(ProduceMethodID.SawPlatform__Barrel_Timber_WoodenAxle__Agitator, "製作攪拌器", new Item[] { new Barrel(1), new Timber(2), new WoodenAxle(1) }, new Object[] { new Agitator() } , 180) },
                { ProduceMethodID.SawPlatform__CircularSawBlade_Timber_SawPlatform__WoodLathe, new ProduceMethod(ProduceMethodID.SawPlatform__CircularSawBlade_Timber_SawPlatform__WoodLathe, "升級成木工車床", new Item[] { new CircularSawBlade(2), new Timber(10) }, new Object[] { new WoodLathe() } ,600) }
            };
        }
        public SawPlatform() { }

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
