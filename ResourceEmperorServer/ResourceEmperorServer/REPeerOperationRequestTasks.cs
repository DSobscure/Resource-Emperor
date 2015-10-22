using System.Collections.Generic;
using Photon.SocketServer;
using REProtocol;
using REStructure;
using Newtonsoft.Json;
using System.Threading;
using REStructure.Scenes;

namespace ResourceEmperorServer
{
    public partial class REPeer : PeerBase
    {
        private void LoginTask(OperationRequest operationRequest)
        {
            if (operationRequest.Parameters.Count != 2)
            {
                OperationResponse response = new OperationResponse(operationRequest.OperationCode)
                {
                    ReturnCode = (short)ErrorType.InvalidParameter,
                    DebugMessage = "LoginTask Parameter Error"
                };
                this.SendOperationResponse(response, new SendParameters());
            }
            else
            {
                string account = (string)operationRequest.Parameters[(byte)LoginParameterItem.Account];
                string password = (string)operationRequest.Parameters[(byte)LoginParameterItem.Password];

                int playerUniqueID;
                if (server.database.LoginCheck(account, password, out playerUniqueID))
                {
                    if (!server.PlayerDictionary.ContainsKey(playerUniqueID))
                    {
                        if (Login(playerUniqueID))
                        {
                            Dictionary<byte, object> parameter = new Dictionary<byte, object>
                                        {
                                            {(byte)LoginResponseItem.PlayerDataString,JsonConvert.SerializeObject(player.Serialize(),new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto })},
                                            {(byte)LoginResponseItem.InventoryDataString,JsonConvert.SerializeObject(player.inventory,new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto })},
                                            {(byte)LoginResponseItem.AppliancesDataString,JsonConvert.SerializeObject(player.appliances,new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto })}
                                        };
                            OperationResponse response = new OperationResponse(operationRequest.OperationCode, parameter)
                            {
                                ReturnCode = (short)ErrorType.Correct,
                                DebugMessage = ""
                            };

                            SendOperationResponse(response, new SendParameters());
                            
                        }
                        else
                        {
                            OperationResponse response = new OperationResponse(operationRequest.OperationCode)
                            {
                                ReturnCode = (short)ErrorType.InvalidOperation,
                                DebugMessage = "登入失敗!"
                            };
                            SendOperationResponse(response, new SendParameters());
                        }
                    }
                    else
                    {
                        OperationResponse response = new OperationResponse(operationRequest.OperationCode)
                        {
                            ReturnCode = (short)ErrorType.InvalidOperation,
                            DebugMessage = "此帳號已經登入!"
                        };
                        SendOperationResponse(response, new SendParameters());
                    }
                }
                else
                {
                    OperationResponse response = new OperationResponse(operationRequest.OperationCode)
                    {
                        ReturnCode = (short)ErrorType.InvalidOperation,
                        DebugMessage = "帳號密碼錯誤!"
                    };
                    SendOperationResponse(response, new SendParameters());
                }
            }
        }
        private void TestTask(OperationRequest operationRequest)
        {
            Inventory inventory = JsonConvert.DeserializeObject<Inventory>((string)operationRequest.Parameters[0], new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
            foreach (Item item in inventory.Values)
            {
                REServer.Log.Info(item.id + " " + item.name + " " + item.description + " " + item.itemCount);
            }
        }
        private async void ProduceTask(OperationRequest operationRequest)
        {
            if (operationRequest.Parameters.Count != 2)
            {
                OperationResponse response = new OperationResponse(operationRequest.OperationCode)
                {
                    ReturnCode = (short)ErrorType.InvalidParameter,
                    DebugMessage = "ProduceTask Parameter Error"
                };
                this.SendOperationResponse(response, new SendParameters());
            }
            else
            {
                ApplianceID applianceID = (ApplianceID)operationRequest.Parameters[(byte)ProduceParameterItem.ApplianceID];
                ProduceMethodID produceMethodID = (ProduceMethodID)operationRequest.Parameters[(byte)ProduceParameterItem.ProduceMethodID];
                if (player.appliances.ContainsKey(applianceID) && player.appliances[applianceID].methods.ContainsKey(produceMethodID))
                {
                    object[] results;
                    if (player.appliances[applianceID].methods[produceMethodID].Process(player.inventory, out results))
                    {
                        await Produce(applianceID,produceMethodID,results, operationRequest.OperationCode);
                    }
                    else
                    {
                        OperationResponse response = new OperationResponse(operationRequest.OperationCode)
                        {
                            ReturnCode = (short)ErrorType.InvalidOperation,
                            DebugMessage = "原料不足"
                        };
                        SendOperationResponse(response, new SendParameters());
                    }
                }
                else
                {
                    OperationResponse response = new OperationResponse(operationRequest.OperationCode)
                    {
                        ReturnCode = (short)ErrorType.InvalidParameter,
                        DebugMessage = "參數錯誤"
                    };
                    SendOperationResponse(response, new SendParameters());
                }
            }
        }
        private void CancelProduceTask()
        {
            cancelTokenSource.Cancel();
            cancelTokenSource.Dispose();
            cancelTokenSource = new CancellationTokenSource();
        }
        private void DiscardItemTask(OperationRequest operationRequest)
        {
            if (operationRequest.Parameters.Count != 2)
            {
                OperationResponse response = new OperationResponse(operationRequest.OperationCode)
                {
                    ReturnCode = (short)ErrorType.InvalidParameter,
                    DebugMessage = "DiscardItemTask Parameter Error"
                };
                this.SendOperationResponse(response, new SendParameters());
            }
            else
            {
                ItemID itemID = (ItemID)operationRequest.Parameters[(byte)DiscardItemParameterItem.ItemID];
                int discardCount = (int)operationRequest.Parameters[(byte)DiscardItemParameterItem.DiscardCount];
                if (player.inventory.ContainsKey(itemID) && player.inventory[itemID].itemCount >= discardCount)
                {
                    player.inventory[itemID].Decrease(discardCount);
                    Dictionary<byte, object> parameter = new Dictionary<byte, object>
                                        {
                                            {(byte)DiscardItemResponseItem.ItemID,itemID},
                                            {(byte)DiscardItemResponseItem.ItemCount,player.inventory[itemID].itemCount},
                                        };
                    if (player.inventory[itemID].itemCount == 0)
                    {
                        player.inventory.Remove(itemID);
                    }
                    OperationResponse response = new OperationResponse(operationRequest.OperationCode, parameter)
                    {
                        ReturnCode = (short)ErrorType.Correct,
                        DebugMessage = ""
                    };
                    SendOperationResponse(response, new SendParameters());
                }
                else
                {
                    OperationResponse response = new OperationResponse(operationRequest.OperationCode)
                    {
                        ReturnCode = (short)ErrorType.InvalidParameter,
                        DebugMessage = "參數錯誤"
                    };
                    SendOperationResponse(response, new SendParameters());
                }
            }
        }
        private void GoToSceneTask(OperationRequest operationRequest)
        {
            if (operationRequest.Parameters.Count != 1)
            {
                OperationResponse response = new OperationResponse(operationRequest.OperationCode)
                {
                    ReturnCode = (short)ErrorType.InvalidParameter,
                    DebugMessage = "GoToSceneTask Parameter Error"
                };
                this.SendOperationResponse(response, new SendParameters());
            }
            else
            {
                int sceneID = (int)operationRequest.Parameters[(byte)GoToSceneParameterItem.TargetSceneID];
                if (server.globalMap.scenes.ContainsKey(sceneID) || sceneID == -1)
                {
                    player.Location = (sceneID == -1) ? workRoom : server.globalMap.scenes[sceneID];
                    Dictionary<byte, object> parameter = new Dictionary<byte, object>
                    {
                        {(byte)GoToSceneResponseItem.TargetSceneID,sceneID}
                    };
                    OperationResponse response = new OperationResponse(operationRequest.OperationCode,parameter)
                    {
                        ReturnCode = (short)ErrorType.Correct,
                        DebugMessage = sceneID.ToString()
                    };
                    SendOperationResponse(response, new SendParameters());
                }
                else
                {
                    OperationResponse response = new OperationResponse(operationRequest.OperationCode)
                    {
                        ReturnCode = (short)ErrorType.InvalidParameter,
                        DebugMessage = "參數錯誤"
                    };
                    SendOperationResponse(response, new SendParameters());
                }
            }
        }
        private void WalkPathTask(OperationRequest operationRequest)
        {
            if (operationRequest.Parameters.Count != 1)
            {
                OperationResponse response = new OperationResponse(operationRequest.OperationCode)
                {
                    ReturnCode = (short)ErrorType.InvalidParameter,
                    DebugMessage = "WalkPathTask Parameter Error"
                };
                this.SendOperationResponse(response, new SendParameters());
            }
            else
            {
                int pathID = (int)operationRequest.Parameters[(byte)WalkPathParameterItem.PathwayID];
                if (server.globalMap.paths.ContainsKey(pathID))
                {
                    Pathway targetPath = server.globalMap.paths[pathID];
                    Scene targetScene = (targetPath.endPoint1 == player.Location)? targetPath.endPoint2 : targetPath.endPoint1;
                    player.Location = targetScene;
                    if(player.Location is Wilderness && targetScene is Wilderness)
                    {
                        Wilderness locationWilderness = player.Location as Wilderness;
                        Wilderness targetWilderness = targetScene as Wilderness;
                        if (!targetWilderness.discoveredPaths.Contains(targetPath))
                        {
                            targetWilderness.discoveredPaths.Add(targetPath);
                        }
                    }
                    else if(player.Location is Town && targetScene is Wilderness)
                    {
                        Wilderness targetWilderness = targetScene as Wilderness;
                        if (!targetWilderness.discoveredPaths.Contains(targetPath))
                        {
                            targetWilderness.discoveredPaths.Add(targetPath);
                        }
                    }
                    Dictionary<byte, object> parameter = new Dictionary<byte, object>
                    {
                        {(byte)WalkPathResponseItem.PathID,pathID},
                        {(byte)WalkPathResponseItem.TargetSceneID,targetScene.uniqueID}
                    };
                    OperationResponse response = new OperationResponse(operationRequest.OperationCode,parameter)
                    {
                        ReturnCode = (short)ErrorType.Correct,
                        DebugMessage = ""
                    };
                    SendOperationResponse(response, new SendParameters());
                }
                else
                {
                    OperationResponse response = new OperationResponse(operationRequest.OperationCode)
                    {
                        ReturnCode = (short)ErrorType.InvalidParameter,
                        DebugMessage = "參數錯誤"
                    };
                    SendOperationResponse(response, new SendParameters());
                }
            }
        }
        private void ExploreTask(OperationRequest operationRequest)
        {
            if (operationRequest.Parameters.Count != 0)
            {
                OperationResponse response = new OperationResponse(operationRequest.OperationCode)
                {
                    ReturnCode = (short)ErrorType.InvalidParameter,
                    DebugMessage = "ExploreTask Parameter Error"
                };
                this.SendOperationResponse(response, new SendParameters());
            }
            else
            {
                if (player.Location is Wilderness)
                {
                    lock(this)
                    {

                    }
                    List<Pathway> paths = (player.Location as Wilderness).Explore();
                    List<int> pathIDs = new List<int>();
                    paths.ForEach(path=> pathIDs.Add(path.uniqueID));
                    Dictionary<byte, object> parameter = new Dictionary<byte, object>
                    {
                        {(byte)ExploreResponseItem.DiscoveredPathIDsDataString,JsonConvert.SerializeObject(pathIDs)}
                    };
                    OperationResponse response = new OperationResponse(operationRequest.OperationCode,parameter)
                    {
                        ReturnCode = (short)ErrorType.Correct,
                        DebugMessage = ""
                    };
                    SendOperationResponse(response, new SendParameters());
                }
                else
                {
                    OperationResponse response = new OperationResponse(operationRequest.OperationCode)
                    {
                        ReturnCode = (short)ErrorType.InvalidOperation,
                        DebugMessage = "此地無法進行探索"
                    };
                    SendOperationResponse(response, new SendParameters());
                }
            }
        }
    }
}
