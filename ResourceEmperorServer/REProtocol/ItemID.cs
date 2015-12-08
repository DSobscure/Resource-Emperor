using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace REProtocol
{
    public enum ItemID : ushort
    {
        MaterialBegin = 0,
        Log = 0,
        Oak,
        Cypress,
        Bamboo,
        Coconut,

        Hemp = 10,

        Cotton = 20,
        RawRubber,

        Clay = 30,

        Rock = 35,
        Sand,

        IronOre = 45,
        CopperOre,

        Coal = 60,

        Brine = 70,
        Water,

        MaterialEnd = 999,

        ToolBegin = 1000,
        StoneAxe = 1000,
        StonePickaxe,
        StoneShovel,

        ToolEnd = 1999,

        ProductBegin = 2000,
        Timber = 2000,
        StoneBlade,
        HempRope,
        Firewood,
        WoodenAxle,
        Barrel,
        OakTimber,
        CypressTimber,
        Brick,
        Charcoal,
        Rubber,
        IronBlock,
        CopperBlock,
        WroughIron,
        Copper,
        CircularSawBlade,
        MetalGear,
        Rivet,
        IronBar,
        IronPipe,
        CopperSheet,
        IronSheet,
        Blade,
        Cottonwool,
        CottonThread,
        CottonRope,
        CottonCloth,
        Paper,
        ProductEnd = 10000,

        No = 65535
    }
}
