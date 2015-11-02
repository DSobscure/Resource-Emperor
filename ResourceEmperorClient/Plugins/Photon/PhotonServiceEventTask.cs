using System.Collections.Generic;
using ExitGames.Client.Photon;
using System;
using REProtocol;
using REStructure;
using REStructure.Items;
using Newtonsoft.Json;

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
                SendMessageEvent(senderName, message);
            }
        }
        else
        {
            DebugReturn(DebugLevel.ERROR,"SendMessageEventTask parameter error");
        }
    }
}
