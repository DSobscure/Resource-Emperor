using RESerializable;
using REProtocol;

public partial class PhotonService
{
    public delegate void ConnectEventHandler(bool connectStatus);
    public event ConnectEventHandler ConnectEvent;

    public delegate void LoginEventHandler(bool loginStatus, string debugMessage, SerializablePlayer player, string inventoryDataString);
    public event LoginEventHandler LoginEvent;

    //event
}
