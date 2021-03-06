﻿using System.Collections.Generic;
using Photon.SocketServer;
using REProtocol;
using REStructure;
using Newtonsoft.Json;
using System.Threading;
using REStructure.Scenes;
using REStructure.Items;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace ResourceEmperorServer
{
    public partial class REPeer : PeerBase
    {
        private void LoginTask(OperationRequest operationRequest)
        {
            if (operationRequest.Parameters.Count != 3)
            {
                OperationResponse response = new OperationResponse(operationRequest.OperationCode)
                {
                    ReturnCode = (short)ErrorType.InvalidParameter,
                    DebugMessage = "LoginTask Parameter Error"
                };
                SendOperationResponse(response, new SendParameters());
            }
            else
            {
                string account = (string)operationRequest.Parameters[(byte)LoginParameterItem.Account];
                string password = (string)operationRequest.Parameters[(byte)LoginParameterItem.Password];
                string version = (string)operationRequest.Parameters[(byte)LoginParameterItem.Version];

                int playerUniqueID;

                if (server.version == version && server.database.LoginCheck(account, password, out playerUniqueID))
                {
                    if (!server.playerDictionary.ContainsKey(playerUniqueID))
                    {
                        if (Login(playerUniqueID))
                        {
                            Dictionary<byte, object> parameter = new Dictionary<byte, object>
                                        {
                                            {(byte)LoginResponseItem.PlayerDataString,JsonConvert.SerializeObject(player.Serialize(),new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto })},
                                            {(byte)LoginResponseItem.InventoryDataString,JsonConvert.SerializeObject(player.inventory,new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto })},
                                            {(byte)LoginResponseItem.AppliancesDataString,JsonConvert.SerializeObject(player.appliances,new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto })},
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
                else if(server.version != version)
                {
                    OperationResponse response = new OperationResponse(operationRequest.OperationCode)
                    {
                        ReturnCode = (short)ErrorType.InvalidOperation,
                        DebugMessage = "請下載最新的版本" + server.version
                    };
                    SendOperationResponse(response, new SendParameters());
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
            foreach (Item item in inventory)
            {
                REServer.log.Info(item.id + " " + item.name + " " + item.description + " " + item.itemCount);
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
                SendOperationResponse(response, new SendParameters());
            }
            else
            {
                ItemID itemID = (ItemID)operationRequest.Parameters[(byte)DiscardItemParameterItem.ItemID];
                int discardCount = (int)operationRequest.Parameters[(byte)DiscardItemParameterItem.DiscardCount];
                if (player.inventory.Any(x=>x.id == itemID) && player.inventory.First(x => x.id == itemID).itemCount >= discardCount)
                {
                    Item discardTarget = player.inventory.First(x => x.id == itemID);
                    player.inventory.Consume(discardTarget.Instantiate(discardCount) as Item);
                    Dictionary<byte, object> parameter = new Dictionary<byte, object>
                                        {
                                            {(byte)DiscardItemResponseItem.ItemID,itemID},
                                            {(byte)DiscardItemResponseItem.ItemCount,player.inventory.Where(x => x.id == itemID).Sum(x=>x.itemCount)},
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
                    if(player.Location != null && player.Location.players.Contains(player))
                        player.Location.players.Remove(player);
                    player.Location = (sceneID == -1) ? workRoom : server.globalMap.scenes[sceneID];
                    player.Location.players.Add(player);
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
                SendOperationResponse(response, new SendParameters());
            }
            else
            {
                int pathID = (int)operationRequest.Parameters[(byte)WalkPathParameterItem.PathwayID];
                if (server.globalMap.paths.ContainsKey(pathID))
                {
                    Pathway targetPath = server.globalMap.paths[pathID];
                    Scene targetScene = (targetPath.endPoint1 == player.Location)? targetPath.endPoint2 : targetPath.endPoint1;
                    if (player.Location != null && player.Location.players.Contains(player))
                        player.Location.players.Remove(player);
                    player.Location = targetScene;
                    player.Location.players.Add(player);
                    Dictionary<byte, object> parameter = new Dictionary<byte, object>
                    {
                        {(byte)WalkPathResponseItem.PathID,pathID},
                        {(byte)WalkPathResponseItem.TargetSceneID,targetScene.uniqueID}
                    };
                    if(targetScene is Wilderness)
                    {
                        parameter.Add((byte)WalkPathResponseItem.Messages, JsonConvert.SerializeObject((targetScene as Wilderness).messages));
                    }
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
                SendOperationResponse(response, new SendParameters());
            }
            else
            {
                if (player.Location is Wilderness)
                {
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
        private void CollectMaterialTask(OperationRequest operationRequest)
        {
            if (operationRequest.Parameters.Count != 2)
            {
                OperationResponse response = new OperationResponse(operationRequest.OperationCode)
                {
                    ReturnCode = (short)ErrorType.InvalidParameter,
                    DebugMessage = "CollectMaterialTask Parameter Error"
                };
                SendOperationResponse(response, new SendParameters());
            }
            else
            {
                CollectionMethod method = (CollectionMethod)operationRequest.Parameters[(byte)CollectMaterialParameterItem.CollectiontMethod];
                ItemID toolID = (ItemID)operationRequest.Parameters[(byte)CollectMaterialParameterItem.ToolID];
                if (player.Location is ResourcePoint && player.inventory.Any(x=>x.id == toolID) || toolID == ItemID.No)
                {
                    Tool tool = (toolID == ItemID.No) ? null : player.inventory.First(x => x.id == toolID) as Tool;
                    ResourcePoint resourcePoint = player.Location as ResourcePoint;
                    if(resourcePoint.collectionList.ContainsKey(method) && resourcePoint.ToolCheck(method,tool))
                    {
                        Item material =  resourcePoint.Collect(method, tool);
                        string resultMessage = null;
                        if(material != null)
                        {
                            player.inventory.Stack(material);
                            resultMessage = "採集到了" + material.itemCount.ToString() + "個" + material.name;
                        }
                        else
                        {
                            resultMessage = "什麼都沒有採到";
                        }
                        Dictionary<byte, object> parameter = new Dictionary<byte, object>
                        {
                            {(byte)CollectMaterialResponseItem.InventoryDataString, JsonConvert.SerializeObject(player.inventory,new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto })}
                        };
                        OperationResponse response = new OperationResponse(operationRequest.OperationCode, parameter)
                        {
                            ReturnCode = (short)ErrorType.Correct,
                            DebugMessage = resultMessage
                        };
                        SendOperationResponse(response, new SendParameters());
                    }
                    else
                    {
                        OperationResponse response = new OperationResponse(operationRequest.OperationCode)
                        {
                            ReturnCode = (short)ErrorType.InvalidOperation,
                            DebugMessage = "採集方法或工具錯誤"
                        };
                        SendOperationResponse(response, new SendParameters());
                    }
                }
                else
                {
                    OperationResponse response = new OperationResponse(operationRequest.OperationCode)
                    {
                        ReturnCode = (short)ErrorType.InvalidOperation,
                        DebugMessage = "此地無法進行採集"
                    };
                    SendOperationResponse(response, new SendParameters());
                }
            }
        }
        private void SendMessageTask(OperationRequest operationRequest)
        {
            if (operationRequest.Parameters.Count != 1)
            {
                OperationResponse response = new OperationResponse(operationRequest.OperationCode)
                {
                    ReturnCode = (short)ErrorType.InvalidParameter,
                    DebugMessage = "SendMessageTask Parameter Error"
                };
                SendOperationResponse(response, new SendParameters());
            }
            else
            {
                string message = (string)operationRequest.Parameters[(byte)SendMessageParameterItem.Message];

                if (SendMessage_and_Broadcast(message))
                {
                    OperationResponse response = new OperationResponse(operationRequest.OperationCode)
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
                        ReturnCode = (short)ErrorType.NotExist,
                        DebugMessage = "Send target not exist"
                    };
                    this.SendOperationResponse(response, new SendParameters());
                }
            }
        }
        private void GetRankingTask(OperationRequest operationRequest)
        {
            if (operationRequest.Parameters.Count != 0)
            {
                OperationResponse response = new OperationResponse(operationRequest.OperationCode)
                {
                    ReturnCode = (short)ErrorType.InvalidParameter,
                    DebugMessage = "GetRankingTask Parameter Error"
                };
                SendOperationResponse(response, new SendParameters());
            }
            else
            {
                Dictionary<string, int> ranking;
                if (server.GetRanking(out ranking))
                {
                    Dictionary<byte, object> parameter = new Dictionary<byte, object>
                    {
                        {(byte)GetRankingResponseItem.RankingDataString,JsonConvert.SerializeObject(ranking)}
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
                        DebugMessage = "資料庫錯誤"
                    };
                    SendOperationResponse(response, new SendParameters());
                }
            }
        }
        private void LeaveMessageTask(OperationRequest operationRequest)
        {
            if (operationRequest.Parameters.Count != 1)
            {
                OperationResponse response = new OperationResponse(operationRequest.OperationCode)
                {
                    ReturnCode = (short)ErrorType.InvalidParameter,
                    DebugMessage = "LeaveMessageTask Parameter Error"
                };
                SendOperationResponse(response, new SendParameters());
            }
            else
            {
                string message = (string)operationRequest.Parameters[(byte)LeaveMessageParameterItem.Message];

                if (player.Location is Wilderness)
                {
                    Wilderness location = player.Location as Wilderness;
                    Task.Run(async delegate
                    {
                        location.LeaveMessage(message);
                        await Task.Delay(new TimeSpan(TimeSpan.TicksPerHour));
                        lock(location.messages)
                        {
                            if (location.messages.Count > 0)
                                location.messages.Dequeue();
                        }
                    });
                    OperationResponse response = new OperationResponse(operationRequest.OperationCode)
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
                        DebugMessage = "here can't leave message"
                    };
                    SendOperationResponse(response, new SendParameters());
                }
            }
        }
        private void TradeCommodityTask(OperationRequest operationRequest)
        {
            if (operationRequest.Parameters.Count != 3)
            {
                OperationResponse response = new OperationResponse(operationRequest.OperationCode)
                {
                    ReturnCode = (short)ErrorType.InvalidParameter,
                    DebugMessage = "TradeCommodityTask Parameter Error"
                };
                SendOperationResponse(response, new SendParameters());
            }
            else
            {
                bool isPurchase = (bool)operationRequest.Parameters[(byte)TradeCommodityItem.IsPurchase];
                ItemID commodityID = (ItemID)(int)operationRequest.Parameters[(byte)TradeCommodityItem.CommodityID];
                int stock = (int)operationRequest.Parameters[(byte)TradeCommodityItem.Count];

                if (player.Location is Town)
                {
                    Market market = (player.Location as Town).market;
                    if (isPurchase)
                    {
                        if(market.Purchase(commodityID, stock, player, player.inventory))
                        {
                            Dictionary<byte, object> parameter = new Dictionary<byte, object>
                            {
                                {(byte)TradeCommodityResponseItem.InventoryDataString, JsonConvert.SerializeObject(player.inventory,new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto })},
                                {(byte)TradeCommodityResponseItem.Money, player.money}
                            };
                            OperationResponse response = new OperationResponse(operationRequest.OperationCode, parameter)
                            {
                                ReturnCode = (short)ErrorType.Correct,
                                DebugMessage = ""
                            };
                            SendOperationResponse(response, new SendParameters());
                            BroadcastMarketChange();
                        }
                        else
                        {
                            OperationResponse response = new OperationResponse(operationRequest.OperationCode)
                            {
                                ReturnCode = (short)ErrorType.InvalidOperation,
                                DebugMessage = "你不能買這個"
                            };
                            SendOperationResponse(response, new SendParameters());
                        }
                    }
                    else
                    {
                        if (market.Sell(commodityID, stock, player, player.inventory))
                        {
                            Dictionary<byte, object> parameter = new Dictionary<byte, object>
                            {
                                {(byte)TradeCommodityResponseItem.InventoryDataString, JsonConvert.SerializeObject(player.inventory,new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto })},
                                {(byte)TradeCommodityResponseItem.Money, player.money}
                            };
                            OperationResponse response = new OperationResponse(operationRequest.OperationCode, parameter)
                            {
                                ReturnCode = (short)ErrorType.Correct,
                                DebugMessage = ""
                            };
                            SendOperationResponse(response, new SendParameters());
                            BroadcastMarketChange();
                        }
                        else
                        {
                            OperationResponse response = new OperationResponse(operationRequest.OperationCode)
                            {
                                ReturnCode = (short)ErrorType.InvalidOperation,
                                DebugMessage = "你不能賣這個"
                            };
                            SendOperationResponse(response, new SendParameters());
                        }
                    }
                }
                else
                {
                    OperationResponse response = new OperationResponse(operationRequest.OperationCode)
                    {
                        ReturnCode = (short)ErrorType.InvalidOperation,
                        DebugMessage = "here can't trade commodity"
                    };
                    this.SendOperationResponse(response, new SendParameters());
                }
            }
        }
        private void GetMarketTask(OperationRequest operationRequest)
        {
            Dictionary<byte, object> parameter = new Dictionary<byte, object>
                            {
                                {(byte)GetMarketResponseItem.MarketDataString, JsonConvert.SerializeObject((player.Location as Town).market ,new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto })},
                            };
            OperationResponse response = new OperationResponse(operationRequest.OperationCode, parameter)
            {
                ReturnCode = (short)ErrorType.Correct,
                DebugMessage = ""
            };
            SendOperationResponse(response, new SendParameters());
        }
    }
}
