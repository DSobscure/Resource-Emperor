using System.Collections.Generic;
using REStructure;
using REProtocol;

public static class GameGlobal
{
    static public ClientPlayer Player;
    static public Inventory Inventory;
    static public Dictionary<ApplianceID, Appliance> Appliances;
    static public bool LoginStatus = false;
    static public GlobalMap GlobalMap;
    static public string version = "0.0.2";
}
