using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace REProtocol
{
    public enum ProduceMethodID
    {
        Machete__Log__Firewood,
        Machete__Log__Timber,
        Machete__Rock__StoneBlade,
        Machete__Hemp__HempRope,
        Machete__StoneBlade_HempRope_Timber__StoneAxe,
        Machete__StoneBlade_HempRope_Timber__StonePickaxe,
        Machete__StoneBlade_HempRope_Timber__StoneShovel,
        Machete__Timber_HempRope__WorkTable,

        WorkTable__Log__WoodenAxle,
        WorkTable__Timber_WorkTable__SawPlatform,

        SawPlatform__Oak__OakTimber,
        SawPlatform__Cypress__CypressTimber,
        SawPlatform__OakTimber__Barrel,
        SawPlatform__Barrel_Timber_WoodenAxle__Agitator,
        SawPlatform__CircularSawBlade_Timber_SawPlatform__WoodLathe,

        WoodLathe__CypressTimber_WoodenAxle__Loom,
        WoodLathe__CypressTimber_WoodenAxle__PaperMachine,

        Agitator__Clay_Rock__StoneStove,

        StoneStove__Log_Firewood__Charcoal,
        StoneStove__Clay_Firewood__Brick,
        StoneStove__Clay_StoneStove__LTKiln,

        LTKiln__RawRubber_Firewood__Rubber,
        LTKiln__Clay_LTKiln__HTKiln,

        HTKiln__IronOre_Coal__IronBlock,
        HTKiln__IronOre_Charcoal__IronBlock,
        HTKiln__CopperOre_Coal__CopperBlock,
        HTKiln__CopperOre_Charcoal__CopperBlock,
        HTKiln__Clay_IronBlock_Coal__Cupola,
        HTKiln__IronBlock_Brick_Timber__BlacksmithPlatform,
        HTKiln__WroughIron_Copper_Timber_Clay__SteelMold,

        Cupola__IronOre_Coal__IronBlock,
        Cupola__IronOre_Charcoal__IronBlock,
        Cupola__CopperOre_Coal__CopperBlock,
        Cupola__CopperOre_Charcoal__CopperBlock,
        Cupola__IronBlock_Coal__WroughIron,
        Cupola__IronBlock_Charcoal__WroughIron,
        Cupola__CopperBlock_Coal__Copper,
        Cupola__CopperBlock_Charcoal__Copper,

        SteelMold__WroughIron__CircularSawBlade,
        SteelMold__WroughIron__MetalGear,
        SteelMold__WroughIron__Rivet,
        SteelMold__WroughIron__IronBar,
        SteelMold__WroughIron__IronPipe,

        BlacksmithPlatform__Copper__CopperSheet,
        BlacksmithPlatform__WroughIron__IronSheet,
        BlacksmithPlatform__WroughIron__Blade
    }
}