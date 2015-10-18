using System.Collections.Generic;
using Photon.SocketServer;
using REProtocol;
using REStructure;
using Newtonsoft.Json;
using System.Threading;

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
                                            {(byte)LoginResponseItem.PlayerDataString,JsonConvert.SerializeObject(Player.Serialize(),new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto })},
                                            {(byte)LoginResponseItem.InventoryDataString,JsonConvert.SerializeObject(Player.inventory,new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto })},
                                            {(byte)LoginResponseItem.AppliancesDataString,JsonConvert.SerializeObject(Player.appliances,new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto })}
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
                if (Player.appliances.ContainsKey(applianceID) && Player.appliances[applianceID].methods.ContainsKey(produceMethodID))
                {
                    object[] results;
                    if (Player.appliances[applianceID].methods[produceMethodID].Process(Player.inventory, out results))
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
                if (Player.inventory.ContainsKey(itemID) && Player.inventory[itemID].itemCount >= discardCount)
                {
                    Player.inventory[itemID].Decrease(discardCount);
                    Dictionary<byte, object> parameter = new Dictionary<byte, object>
                                        {
                                            {(byte)DiscardItemResponseItem.ItemID,itemID},
                                            {(byte)DiscardItemResponseItem.ItemCount,Player.inventory[itemID].itemCount},
                                        };
                    if (Player.inventory[itemID].itemCount == 0)
                    {
                        Player.inventory.Remove(itemID);
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
    }
}
