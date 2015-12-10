using System.Collections.Generic;
using ExitGames.Client.Photon;
using System;
using REProtocol;
using REStructure;
using REStructure.Items;
using Newtonsoft.Json;
using REStructure.Scenes;

public partial class PhotonService : IPhotonPeerListener
{
    private void SendMessageEventTask(EventData eventData)
    {
        if (eventData.Parameters.Count == 3)
        {
            int sceneID = (int)eventData.Parameters[(byte)SendMessageBroadcastItem.SceneID];
            string senderName = (string)eventData.Parameters[(byte)SendMessageBroadcastItem.PlayerName];
            string message = (string)eventData.Parameters[(byte)SendMessageBroadcastItem.Message];
            if(sceneID == GameGlobal.Player.Location.uniqueID)
            {
                SendMessageEvent(true, "",senderName, message);
            }
        }
        else
        {
            SendMessageEvent(false, "SendMessageEventTask parameter error", "", "");
        }
    }
    private void MarketChangeEventTask(EventData eventData)
    {
        if (GameGlobal.Player.Location is Town && eventData.Parameters.Count == 1)
        {
            (GameGlobal.Player.Location as Town).market.Update(JsonConvert.DeserializeObject<Market>((string)eventData.Parameters[(byte)MarketChangeBroadcastItem.MarketDataString], new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto }));
        }
    }
}
