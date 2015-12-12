using System.Collections.Generic;
using REProtocol;
using REStructure;
using System;

public partial class PhotonService
{
    public event Action<string> OnDebugReturn;

    public delegate void ConnectResponseEventHandler(bool status);
    public event ConnectResponseEventHandler OnConnectResponse;

    public event Action<string> OnAlert;

    public delegate void LoginResponseEventHandler(bool status);
    public event LoginResponseEventHandler OnLoginResponse;

    public delegate void ProduceResponseEventHandler(bool status, ApplianceID applianceID, ProduceMethodID produceMethodID, ApplianceID selectedApplianceID, bool isNewAppliance);
    public event ProduceResponseEventHandler OnProduceResponse;

    public delegate void DiscardItemResponseEventHandler(bool status, ItemID itemid, int itemCount);
    public event DiscardItemResponseEventHandler OnDiscardItemResponse;

    public delegate void GoToSceneResponseEventHandler(bool status, Scene targetScene);
    public event GoToSceneResponseEventHandler OnGoToSceneResponse;

    public delegate void WalkPathResponseEventHandler(bool status, Pathway path, Scene targetScene, List<string> messages);
    public event WalkPathResponseEventHandler OnWalkPathResponse;

    public delegate void ExploreResponseEventHandler(bool status, List<Pathway> paths);
    public event ExploreResponseEventHandler OnExploreResponse;

    public delegate void CollectMaterialResponseEventHandler(bool status, string resultMessage);
    public event CollectMaterialResponseEventHandler OnCollectMaterialResponse;

    public delegate void SendMessageResponseEventHandler(bool status, string senderName, string message);
    public event SendMessageResponseEventHandler OnSendMessageResponse;

    public delegate void GetRankingResponseEventHandler(bool status, Dictionary<string, int> ranking);
    public event GetRankingResponseEventHandler OnGetRankingResponse;
}
