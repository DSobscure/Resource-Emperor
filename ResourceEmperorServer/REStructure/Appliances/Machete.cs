using System;
using System.Collections.Generic;
using REProtocol;
using REStructure.Items.Materials;
using REStructure.Items.Products;
using REStructure.Items.Tools;

namespace REStructure.Appliances
{
    public class Machete : Appliance
    {
        static ApplianceID _id;
        static string _name;
        static Dictionary<ProduceMethodID, ProduceMethod> _methods;

        static Machete()
        {
            _id = ApplianceID.Machete;
            _name = "柴刀";
            _methods = new Dictionary<ProduceMethodID, ProduceMethod>()
            {
                { ProduceMethodID.Machete__Log__Firewood, new ProduceMethod(ProduceMethodID.Machete__Log__Firewood, "製作柴薪", new Item[]{ new Log(1) }, new Object[]{ new Firewood(3) }, 0) },
                { ProduceMethodID.Machete__Log__Timber, new ProduceMethod(ProduceMethodID.Machete__Log__Timber, "製作木材", new Item[] { new Log(1) }, new Object[] { new Timber(1) },0) },
                { ProduceMethodID.Machete__Rock__StoneBlade, new ProduceMethod(ProduceMethodID.Machete__Rock__StoneBlade, "製作石刀片", new Item[] { new Rock(2) }, new Object[] { new StoneBlade(1) } ,0) },
                { ProduceMethodID.Machete__Hemp__HempRope, new ProduceMethod(ProduceMethodID.Machete__Hemp__HempRope, "製作麻繩", new Item[] { new Hemp(4) }, new Object[] { new HempRope(1) } ,30) },
                { ProduceMethodID.Machete__StoneBlade_HempRope_Timber__StoneAxe, new ProduceMethod(ProduceMethodID.Machete__StoneBlade_HempRope_Timber__StoneAxe, "製作石斧", new Item[] { new StoneBlade(1), new HempRope(1), new Timber(1) }, new Object[] { new StoneAxe(1,100,100) } ,60) },
                { ProduceMethodID.Machete__StoneBlade_HempRope_Timber__StonePickaxe, new ProduceMethod(ProduceMethodID.Machete__StoneBlade_HempRope_Timber__StonePickaxe, "製作石鎬", new Item[] { new StoneBlade(1), new HempRope(1), new Timber(1) }, new Object[] { new StonePickaxe(1,100,100) } ,60) },
                { ProduceMethodID.Machete__StoneBlade_HempRope_Timber__StoneShovel, new ProduceMethod(ProduceMethodID.Machete__StoneBlade_HempRope_Timber__StoneShovel, "製作石鏟", new Item[] { new StoneBlade(1), new HempRope(1), new Timber(1) }, new Object[] { new StoneShovel(1,100,100) } ,60) },
                { ProduceMethodID.Machete__Timber_HempRope__WorkTable, new ProduceMethod(ProduceMethodID.Machete__Timber_HempRope__WorkTable, "升級成工作桌", new Item[] { new Timber(10), new HempRope(4) }, new Object[] { new WorkTable() } ,300) }
            };
        }
        public Machete() { }

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
