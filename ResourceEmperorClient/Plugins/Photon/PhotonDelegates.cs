using System.Collections.Generic;
using RESerializable;
using REProtocol;
using REStructure;

public partial class PhotonService
{
    public delegate void ConnectEventHandler(bool connectStatus);
    public event ConnectEventHandler ConnectEvent;

    public delegate void LoginEventHandler(bool loginStatus, string debugMessage, SerializablePlayer player, Inventory inventory, Dictionary<ApplianceID,Appliance> appliances);
    public event LoginEventHandler LoginEvent;

    public delegate void ProduceEventHandler(bool produceStatus, string debugMessage, ApplianceID applianceID, ProduceMethodID produceMethodID);
    public event ProduceEventHandler ProduceEvent;

    //event
}
