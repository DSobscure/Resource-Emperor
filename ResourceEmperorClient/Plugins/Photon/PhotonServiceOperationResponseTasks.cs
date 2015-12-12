using System.Collections.Generic;
using ExitGames.Client.Photon;
using REProtocol;
using REStructure;
using Newtonsoft.Json;
using REStructure.Scenes;
using System;

public partial class PhotonService : IPhotonPeerListener
{
    private void LoginResponseTask(OperationResponse operationResponse)
    {
        try
        {
            if (operationResponse.ReturnCode == (short)ErrorType.Correct)
            {
                Player player = JsonConvert.DeserializeObject<Player>((string)operationResponse.Parameters[(byte)LoginResponseItem.PlayerDataString], new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
                Inventory inventory = JsonConvert.DeserializeObject<Inventory>((string)operationResponse.Parameters[(byte)LoginResponseItem.InventoryDataString], new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
                Dictionary<ApplianceID, Appliance> appliances = JsonConvert.DeserializeObject<Dictionary<ApplianceID, Appliance>>((string)operationResponse.Parameters[(byte)LoginResponseItem.AppliancesDataString], new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });

                GameGlobal.LoginStatus = true;
                GameGlobal.Player = new ClientPlayer(player);
                GameGlobal.Inventory = inventory;
                GameGlobal.Appliances = appliances;
                GameGlobal.GlobalMap = new GlobalMap();
                GameGlobal.Player.Location = new Room("WorkRoomScene");
                if (OnLoginResponse != null)
                    OnLoginResponse(true);
            }
            else
            {
                GameGlobal.LoginStatus = false;
                if (OnAlert != null)
                    OnAlert(operationResponse.DebugMessage);
                if (OnLoginResponse != null)
                    OnLoginResponse(false);
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }
    private void ProduceResponseTask(OperationResponse operationResponse)
    {
        try
        {
            GameGlobal.Player.IsWorking = false;
            if (operationResponse.ReturnCode == (short)ErrorType.Correct)
            {
                ApplianceID applianceID = (ApplianceID)operationResponse.Parameters[(byte)ProduceResponseItem.ApplianceID];
                ProduceMethodID produceMethodID = (ProduceMethodID)operationResponse.Parameters[(byte)ProduceResponseItem.ProduceMethodID];
                ApplianceID selectedApplianceID = applianceID;
                bool isNewAppliance = false;

                if (GameGlobal.Appliances.ContainsKey(applianceID) && GameGlobal.Appliances[applianceID].methods.ContainsKey(produceMethodID))
                {
                    object[] results;
                    Appliance appliance = GameGlobal.Appliances[applianceID];
                    ProduceMethod produceMethod = appliance.methods[produceMethodID];
                    
                    if (produceMethod.Process(GameGlobal.Inventory, out results))
                    {
                        foreach (object result in results)
                        {
                            if (result is Item)
                            {
                                Item item = result as Item;
                                GameGlobal.Inventory.Stack(item);
                            }
                            else if (result is Appliance)
                            {
                                Appliance newAppliance = result as Appliance;
                                if (!GameGlobal.Appliances.ContainsKey(newAppliance.id))
                                {
                                    if (appliance is IUpgradable)
                                    {
                                        IUpgradable target = appliance as IUpgradable;
                                        if (target.UpgradeCheck(newAppliance))
                                        {
                                            GameGlobal.Appliances.Remove(appliance.id);
                                            GameGlobal.Appliances.Add(newAppliance.id, newAppliance);
                                        }
                                        else
                                        {
                                            GameGlobal.Appliances.Add(newAppliance.id, newAppliance);
                                        }
                                    }
                                    else
                                    {
                                        GameGlobal.Appliances.Add(newAppliance.id, newAppliance);
                                    }
                                }
                                selectedApplianceID = newAppliance.id;
                                isNewAppliance = true;
                            }
                        }
                    }
                }

                if (OnProduceResponse != null)
                    OnProduceResponse(true, applianceID, produceMethodID, selectedApplianceID, isNewAppliance);
            }
            else
            {
                if (OnAlert != null)
                    OnAlert(operationResponse.DebugMessage);
                if (OnProduceResponse != null)
                    OnProduceResponse(false, 0, 0, 0, false);
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }
    private void DiscardItemResponseTask(OperationResponse operationResponse)
    {
        try
        {
            if (operationResponse.ReturnCode == (short)ErrorType.Correct)
            {
                ItemID itemID = (ItemID)operationResponse.Parameters[(byte)DiscardItemResponseItem.ItemID];
                int itemCount = (int)operationResponse.Parameters[(byte)DiscardItemResponseItem.ItemCount];
                if(OnDiscardItemResponse != null)
                    OnDiscardItemResponse(true, itemID, itemCount);
            }
            else
            {
                if (OnAlert != null)
                    OnAlert(operationResponse.DebugMessage);
                if (OnDiscardItemResponse != null)
                    OnDiscardItemResponse(false, 0, 0);
            }
        }
        catch (Exception ex)
        {
            DebugReturn(DebugLevel.ERROR, ex.Message);
            DebugReturn(DebugLevel.ERROR, ex.StackTrace);
        }
    }
    private void GoToSceneResponseTask(OperationResponse operationResponse)
    {
        try
        {
            if (operationResponse.ReturnCode == (short)ErrorType.Correct)
            {
                int targetSceneID = (int)operationResponse.Parameters[(byte)GoToSceneResponseItem.TargetSceneID];
                if (GameGlobal.GlobalMap.scenes.ContainsKey(targetSceneID))
                {
                    if (OnGoToSceneResponse != null)
                        OnGoToSceneResponse(true, GameGlobal.GlobalMap.scenes[targetSceneID]);
                }
                else if (targetSceneID == -1)
                {
                    if (OnGoToSceneResponse != null)
                        OnGoToSceneResponse(true, new Room("WorkRoomScene"));
                }
                else
                {
                    if (OnAlert != null)
                        OnAlert(operationResponse.DebugMessage);
                    if (OnGoToSceneResponse != null)
                        OnGoToSceneResponse(false, null);
                }
            }
            else
            {
                if (OnAlert != null)
                    OnAlert(operationResponse.DebugMessage);
                if (OnGoToSceneResponse != null)
                    OnGoToSceneResponse(false, null);
            }
        }
        catch (Exception ex)
        {
            DebugReturn(DebugLevel.ERROR, ex.Message);
            DebugReturn(DebugLevel.ERROR, ex.StackTrace);
        }
    }
    private void WalkPathResponseTask(OperationResponse operationResponse)
    {
        try
        {
            if (operationResponse.ReturnCode == (short)ErrorType.Correct)
            {
                int pathID = (int)operationResponse.Parameters[(byte)WalkPathResponseItem.PathID];
                int targetScene = (int)operationResponse.Parameters[(byte)WalkPathResponseItem.TargetSceneID];
                if (GameGlobal.GlobalMap.paths.ContainsKey(pathID) && GameGlobal.GlobalMap.scenes.ContainsKey(targetScene))
                {
                    if (GameGlobal.GlobalMap.scenes[targetScene] is Wilderness)
                    {
                        List<string> messages = JsonConvert.DeserializeObject<List<string>>((string)operationResponse.Parameters[(byte)WalkPathResponseItem.Messages]);
                        if (OnWalkPathResponse != null)
                            OnWalkPathResponse(true, GameGlobal.GlobalMap.paths[pathID], GameGlobal.GlobalMap.scenes[targetScene], messages);
                    }
                    else
                    {
                        if (OnWalkPathResponse != null)
                            OnWalkPathResponse(true, GameGlobal.GlobalMap.paths[pathID], GameGlobal.GlobalMap.scenes[targetScene], null);
                    }
                }
                else
                {
                    if (OnAlert != null)
                        OnAlert(operationResponse.DebugMessage);
                    if (OnWalkPathResponse != null)
                        OnWalkPathResponse(false, null, null, null);
                }
            }
            else
            {
                if (OnAlert != null)
                    OnAlert(operationResponse.DebugMessage);
                if (OnWalkPathResponse != null)
                    OnWalkPathResponse(false, null, null, null);
            }
        }
        catch (Exception ex)
        {
            DebugReturn(DebugLevel.ERROR, ex.Message);
            DebugReturn(DebugLevel.ERROR, ex.StackTrace);
        }
    }
    private void ExploreResponseTask(OperationResponse operationResponse)
    {
        try
        {
            if (operationResponse.ReturnCode == (short)ErrorType.Correct)
            {
                List<int> pathIDs = JsonConvert.DeserializeObject<List<int>>((string)operationResponse.Parameters[(byte)ExploreResponseItem.DiscoveredPathIDsDataString]);
                List<Pathway> paths = new List<Pathway>();
                foreach (int pathID in pathIDs)
                {
                    if (GameGlobal.GlobalMap.paths.ContainsKey(pathID))
                    {
                        paths.Add(GameGlobal.GlobalMap.paths[pathID]);
                    }
                }
                if (OnExploreResponse != null)
                    OnExploreResponse(true, paths);
            }
            else
            {
                if (OnAlert != null)
                    OnAlert(operationResponse.DebugMessage);
                if (OnExploreResponse != null)
                    OnExploreResponse(false, null);
            }
        }
        catch (Exception ex)
        {
            DebugReturn(DebugLevel.ERROR, ex.Message);
            DebugReturn(DebugLevel.ERROR, ex.StackTrace);
        }
    }
    private void CollectMaterialResponseTask(OperationResponse operationResponse)
    {
        try
        {
            if (operationResponse.ReturnCode == (short)ErrorType.Correct)
            {
                GameGlobal.Inventory = JsonConvert.DeserializeObject<Inventory>((string)operationResponse.Parameters[(byte)CollectMaterialResponseItem.InventoryDataString], new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
                if (OnCollectMaterialResponse != null)
                    OnCollectMaterialResponse(true, operationResponse.DebugMessage);
            }
            else
            {
                if (OnCollectMaterialResponse != null)
                    OnCollectMaterialResponse(false, operationResponse.DebugMessage);
            }
        }
        catch (Exception ex)
        {
            DebugReturn(DebugLevel.ERROR, ex.Message);
            DebugReturn(DebugLevel.ERROR, ex.StackTrace);
        }
    }
    private void SendMessageResponseTask(OperationResponse operationResponse)
    {
        try
        {
            if (operationResponse.ReturnCode != (short)ErrorType.Correct)
            {
                if (OnSendMessageResponse != null)
                    OnSendMessageResponse(false, "system", "error");
            }
        }
        catch (Exception ex)
        {
            DebugReturn(DebugLevel.ERROR, ex.Message);
            DebugReturn(DebugLevel.ERROR, ex.StackTrace);
        }
    }
    private void GetRankingResponseTask(OperationResponse operationResponse)
    {
        try
        {
            if (operationResponse.ReturnCode == (short)ErrorType.Correct)
            {
                if (OnGetRankingResponse != null)
                    OnGetRankingResponse(true, JsonConvert.DeserializeObject<Dictionary<string, int>>((string)operationResponse.Parameters[(byte)GetRankingResponseItem.RankingDataString]));
            }
            else
            {
                if (OnAlert != null)
                    OnAlert(operationResponse.DebugMessage);
            }
        }
        catch (Exception ex)
        {
            DebugReturn(DebugLevel.ERROR, ex.Message);
            DebugReturn(DebugLevel.ERROR, ex.StackTrace);
        }
    }
    private void LeaveMessageResponseTask(OperationResponse operationResponse)
    {
        try
        {
            if (operationResponse.ReturnCode != (short)ErrorType.Correct)
            {
                if(OnAlert != null)
                    OnAlert(operationResponse.DebugMessage);
            }
        }
        catch (Exception ex)
        {
            DebugReturn(DebugLevel.ERROR, ex.Message);
            DebugReturn(DebugLevel.ERROR, ex.StackTrace);
        }
    }
    private void TradeCommodityResponseTask(OperationResponse operationResponse)
    {
        try
        {
            if (operationResponse.ReturnCode == (short)ErrorType.Correct)
            {
                GameGlobal.Inventory.Update(JsonConvert.DeserializeObject<Inventory>((string)operationResponse.Parameters[(byte)TradeCommodityResponseItem.InventoryDataString], new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto }));
                GameGlobal.Player.money = (int)operationResponse.Parameters[(byte)TradeCommodityResponseItem.Money];
            }
            else
            {
                if (OnAlert != null)
                    OnAlert(operationResponse.DebugMessage);
            }
        }
        catch(Exception ex)
        {
            DebugReturn(DebugLevel.ERROR, ex.Message);
            DebugReturn(DebugLevel.ERROR, ex.StackTrace);
        }
    }
    private void GetMarketResponseTask(OperationResponse operationResponse)
    {
        try
        {
            if (operationResponse.ReturnCode == (short)ErrorType.Correct)
            {
                if (GameGlobal.Player.Location is Town)
                {
                    (GameGlobal.Player.Location as Town).market.Update(JsonConvert.DeserializeObject<Market>((string)operationResponse.Parameters[(byte)MarketChangeBroadcastItem.MarketDataString], new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto }));
                }
            }
        }
        catch (Exception ex)
        {
            DebugReturn(DebugLevel.ERROR, ex.Message);
            DebugReturn(DebugLevel.ERROR, ex.StackTrace);
        }
    }
}
