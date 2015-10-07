using System.Collections.Generic;
using REStructure;
using REProtocol;

public static class PlayerGlobal
{
    static public Player Player;
    static public Inventory Inventory;
    static public Dictionary<ApplianceID, Appliance> Appliances;
    static public bool LoginStatus = false;
}
