using System.Collections.Generic;
using ExitGames.Client.Photon;
using REProtocol;
using REStructure;
using Newtonsoft.Json;
using REStructure.Scenes;

public partial class PhotonService : IPhotonPeerListener
{
    private void LoginTask(OperationResponse operationResponse)
    {
        if (operationResponse.ReturnCode == (short)ErrorType.Correct)
        {
            string version = (string)operationResponse.Parameters[(byte)LoginResponseItem.Version];
            if(version != GameGlobal.version)
            {
                LoginEvent(false, "請下載最新版本: "+version, null, null, null);
            }
            LoginEvent
            (
                status: true,
                debugMessage: "",
                player: JsonConvert.DeserializeObject<Player>((string)operationResponse.Parameters[(byte)LoginResponseItem.PlayerDataString], new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto }),
                inventory: JsonConvert.DeserializeObject<Inventory>((string)operationResponse.Parameters[(byte)LoginResponseItem.InventoryDataString], new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto }),
                appliances: JsonConvert.DeserializeObject<Dictionary<ApplianceID, Appliance>>((string)operationResponse.Parameters[(byte)LoginResponseItem.AppliancesDataString], new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto })
            );
        }
        else
        {
            DebugReturn(0, operationResponse.DebugMessage);
            LoginEvent(false, operationResponse.DebugMessage, null, null, null);
        }
    }
    private void ProduceTask(OperationResponse operationResponse)
    {
        if (operationResponse.ReturnCode == (short)ErrorType.Correct)
        {
            ApplianceID applianceID = (ApplianceID)operationResponse.Parameters[(byte)ProduceResponseItem.ApplianceID];
            ProduceMethodID produceMethodID = (ProduceMethodID)operationResponse.Parameters[(byte)ProduceResponseItem.ProduceMethodID];
            ProduceEvent(true, operationResponse.DebugMessage, applianceID, produceMethodID);
        }
        else if (operationResponse.ReturnCode == (short)ErrorType.Canceled)
        {
            ProduceEvent(false, operationResponse.DebugMessage, 0, 0);
        }
        else
        {
            DebugReturn(0, operationResponse.DebugMessage);
            ProduceEvent(false, operationResponse.DebugMessage, 0, 0);
        }
    }
    private void DiscardItemTask(OperationResponse operationResponse)
    {
        if (operationResponse.ReturnCode == (short)ErrorType.Correct)
        {
            ItemID itemID = (ItemID)operationResponse.Parameters[(byte)DiscardItemResponseItem.ItemID];
            int itemCount = (int)operationResponse.Parameters[(byte)DiscardItemResponseItem.ItemCount];
            DiscardItemEvent(true, operationResponse.DebugMessage, itemID, itemCount);
        }
        else
        {
            DebugReturn(0, operationResponse.DebugMessage);
            DiscardItemEvent(false, operationResponse.DebugMessage, 0, 0);
        }
    }
    private void GoToSceneTask(OperationResponse operationResponse)
    {
        if (operationResponse.ReturnCode == (short)ErrorType.Correct)
        {
            int targetSceneID = (int)operationResponse.Parameters[(byte)GoToSceneResponseItem.TargetSceneID];
            if(GameGlobal.GlobalMap.scenes.ContainsKey(targetSceneID))
            {
                GoToSceneEvent(true, operationResponse.DebugMessage, GameGlobal.GlobalMap.scenes[targetSceneID]);
            }
            else if(targetSceneID == -1)
            {
                GoToSceneEvent(true, operationResponse.DebugMessage, new Room("WorkRoomScene"));
            }
            else
            {
                DebugReturn(0, operationResponse.DebugMessage);
                GoToSceneEvent(false, "目標場景不存在", null);
            }
        }
        else
        {
            DebugReturn(0, operationResponse.DebugMessage);
            GoToSceneEvent(false, operationResponse.DebugMessage,null);
        }
    }
    private void WalkPathTask(OperationResponse operationResponse)
    {
        if (operationResponse.ReturnCode == (short)ErrorType.Correct)
        {
            int pathID = (int)operationResponse.Parameters[(byte)WalkPathResponseItem.PathID];
            int targetScene = (int)operationResponse.Parameters[(byte)WalkPathResponseItem.TargetSceneID];
            if(GameGlobal.GlobalMap.paths.ContainsKey(pathID) && GameGlobal.GlobalMap.scenes.ContainsKey(targetScene))
            {
                if(GameGlobal.GlobalMap.scenes[targetScene] is Wilderness)
                {
                    List<string> messages = JsonConvert.DeserializeObject<List<string>>((string)operationResponse.Parameters[(byte)WalkPathResponseItem.Messages]);
                    WalkPathEvent(true, operationResponse.DebugMessage, GameGlobal.GlobalMap.paths[pathID], GameGlobal.GlobalMap.scenes[targetScene], messages);
                }
                else
                {
                    WalkPathEvent(true, operationResponse.DebugMessage, GameGlobal.GlobalMap.paths[pathID], GameGlobal.GlobalMap.scenes[targetScene], null);
                }
            }
            else
            {
                DebugReturn(0, operationResponse.DebugMessage);
                WalkPathEvent(false, "target scene not in global map", null, null, null);
            }
        }
        else
        {
            DebugReturn(0, operationResponse.DebugMessage);
            WalkPathEvent(false, operationResponse.DebugMessage, null, null, null);
        }
    }
    private void ExploreTask(OperationResponse operationResponse)
    {
        if (operationResponse.ReturnCode == (short)ErrorType.Correct)
        {
            List<int> pathIDs = JsonConvert.DeserializeObject<List<int>>((string)operationResponse.Parameters[(byte)ExploreResponseItem.DiscoveredPathIDsDataString]);
            List<Pathway> paths = new List<Pathway>();
            foreach (int pathID in pathIDs)
            {
                if(GameGlobal.GlobalMap.paths.ContainsKey(pathID))
                {
                    paths.Add(GameGlobal.GlobalMap.paths[pathID]);
                }
            }
            ExploreEvent(true, operationResponse.DebugMessage, paths);
        }
        else
        {
            DebugReturn(0, operationResponse.DebugMessage);
            ExploreEvent(false, operationResponse.DebugMessage, null);
        }
    }
    private void CollectMaterialTask(OperationResponse operationResponse)
    {
        if (operationResponse.ReturnCode == (short)ErrorType.Correct)
        {
            GameGlobal.Inventory = JsonConvert.DeserializeObject<Inventory>((string)operationResponse.Parameters[(byte)CollectMaterialResponseItem.InventoryDataString], new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
            CollectMaterialEvent(true, operationResponse.DebugMessage);
        }
        else
        {
            DebugReturn(0, operationResponse.DebugMessage);
            CollectMaterialEvent(false, operationResponse.DebugMessage);
        }
    }
    private void SendMessageTask(OperationResponse operationResponse)
    {
        if (operationResponse.ReturnCode != (short)ErrorType.Correct)
        {
            SendMessageEvent(false, operationResponse.DebugMessage, "system","error");
        }
    }
    private void GetRankingTask(OperationResponse operationResponse)
    {
        if (operationResponse.ReturnCode == (short)ErrorType.Correct)
        {
            GetRankingEvent(true, operationResponse.DebugMessage, JsonConvert.DeserializeObject<Dictionary<string,int>>((string)operationResponse.Parameters[(byte)GetRankingResponseItem.RankingDataString]));
        }
        else
        {
            GetRankingEvent(false, operationResponse.DebugMessage, null);
        }
    }
    private void LeaveMessageTask(OperationResponse operationResponse)
    {
        if (operationResponse.ReturnCode != (short)ErrorType.Correct)
        {
            DebugReturn(DebugLevel.ERROR, operationResponse.DebugMessage);
        }
    }
}
