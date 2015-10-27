using System.Collections.Generic;
using REProtocol;
using REStructure;

public partial class PhotonService
{
    public delegate void ConnectEventHandler(bool status);
    public event ConnectEventHandler ConnectEvent;

    public delegate void LoginEventHandler(bool status, string debugMessage, Player player, Inventory inventory, Dictionary<ApplianceID,Appliance> appliances);
    public event LoginEventHandler LoginEvent;

    public delegate void ProduceEventHandler(bool status, string debugMessage, ApplianceID applianceID, ProduceMethodID produceMethodID);
    public event ProduceEventHandler ProduceEvent;

    public delegate void DiscardItemEventHandler(bool status, string debugMessage, ItemID itemid, int itemCount);
    public event DiscardItemEventHandler DiscardItemEvent;

    public delegate void GoToSceneEventHandler(bool status, string debugMessage, Scene targetScene);
    public event GoToSceneEventHandler GoToSceneEvent;

    public delegate void WalkPathEventHandler(bool status, string debugMessage, Pathway path, Scene targetScene);
    public event WalkPathEventHandler WalkPathEvent;

    public delegate void ExploreEventHandler(bool status, string debugMessage, List<Pathway> paths);
    public event ExploreEventHandler ExploreEvent;

    public delegate void CollectMaterialEventHandler(bool status, string debugMessage);
    public event CollectMaterialEventHandler CollectMaterialEvent;
}
