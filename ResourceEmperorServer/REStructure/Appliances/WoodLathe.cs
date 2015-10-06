using System;
using System.Collections.Generic;
using REProtocol;
using REStructure.Items.Materials;
using REStructure.Items.Products;
using REStructure.Items.Tools;

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
                { ProduceMethodID.Machete__Log__Firewood, new ProduceMethod(ProduceMethodID.Machete__Log__Firewood, "製作柴薪", new Item[]{ new Log(1) }, new Object[]{ new Firewood(3) }, 0) },
                { ProduceMethodID.Machete__Log__Timber, new ProduceMethod(ProduceMethodID.Machete__Log__Timber, "製作木材", new Item[] { new Log(1) }, new Object[] { new Timber(1) },0) },
                { ProduceMethodID.Machete__Rock__StoneBlade, new ProduceMethod(ProduceMethodID.Machete__Rock__StoneBlade, "製作石刀片", new Item[] { new Rock(2) }, new Object[] { new StoneBlade(1) } ,0) },
                { ProduceMethodID.Machete__Hemp__HempRope, new ProduceMethod(ProduceMethodID.Machete__Hemp__HempRope, "製作麻繩", new Item[] { new Hemp(4) }, new Object[] { new HempRope(1) } ,30) },
                { ProduceMethodID.Machete__StoneBlade_HempRope_Timber__StoneAxe, new ProduceMethod(ProduceMethodID.Machete__StoneBlade_HempRope_Timber__StoneAxe, "製作石斧", new Item[] { new StoneBlade(1), new HempRope(1), new Timber(1) }, new Object[] { new StoneAxe(1,100,100) } ,20) },
                { ProduceMethodID.Machete__StoneBlade_HempRope_Timber__StonePickaxe, new ProduceMethod(ProduceMethodID.Machete__StoneBlade_HempRope_Timber__StonePickaxe, "製作石鎬", new Item[] { new StoneBlade(1), new HempRope(1), new Timber(1) }, new Object[] { new StonePickaxe(1,100,100) } , 20) },
                { ProduceMethodID.Machete__StoneBlade_HempRope_Timber__StoneShovel, new ProduceMethod(ProduceMethodID.Machete__StoneBlade_HempRope_Timber__StoneShovel, "製作石鏟", new Item[] { new StoneBlade(1), new HempRope(1), new Timber(1) }, new Object[] { new StoneShovel(1,100,100) } , 20) },

                { ProduceMethodID.WorkTable__Log__WoodenAxle, new ProduceMethod(ProduceMethodID.WorkTable__Log__WoodenAxle, "製作木製輪軸", new Item[]{ new Log(1) }, new Object[]{ new WoodenAxle(1) }, 20) },

                { ProduceMethodID.SawPlatform__Oak__OakTimber, new ProduceMethod(ProduceMethodID.SawPlatform__Oak__OakTimber, "製作橡木材", new Item[]{ new Oak(1) }, new Object[]{ new OakTimber(1) }, 0) },
                { ProduceMethodID.SawPlatform__Cypress__CypressTimber, new ProduceMethod(ProduceMethodID.SawPlatform__Cypress__CypressTimber, "製作檜木材", new Item[] { new Cypress(1) }, new Object[] { new CypressTimber(1) }, 0) },
                { ProduceMethodID.SawPlatform__OakTimber__Barrel, new ProduceMethod(ProduceMethodID.SawPlatform__OakTimber__Barrel, "製作木桶", new Item[] { new OakTimber(5) }, new Object[] { new Barrel(1) } , 30) },
                { ProduceMethodID.SawPlatform__Barrel_Timber_WoodenAxle__Agitator, new ProduceMethod(ProduceMethodID.SawPlatform__Barrel_Timber_WoodenAxle__Agitator, "製作攪拌器", new Item[] { new Barrel(1), new Timber(2), new WoodenAxle(1) }, new Object[] { new Agitator() } , 100) },

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
