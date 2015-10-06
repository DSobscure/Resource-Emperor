using RESerializable;
using REProtocol;
using REStructure;

public partial class PhotonService
{
    public delegate void ConnectEventHandler(bool connectStatus);
    public event ConnectEventHandler ConnectEvent;

    public delegate void LoginEventHandler(bool loginStatus, string debugMessage, SerializablePlayer player, Inventory inventory);
    public event LoginEventHandler LoginEvent;

    //event
}
