using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using REProtocol;
using RESerializable;
using REStructure.Items.Materials;
using REStructure;
using Newtonsoft.Json;
using System.Reflection;
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
        private void ProduceTask(OperationRequest operationRequest)
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
                        Thread.Sleep(Player.appliances[applianceID].methods[produceMethodID].processTime * 100);
                        foreach (object result in results)
                        {
                            if (result is Item)
                            {
                                Item item = result as Item;
                                if (Player.inventory.ContainsKey(item.id))
                                {
                                    Player.inventory[item.id].Increase(item.itemCount);
                                }
                                else
                                {
                                    Player.inventory.Add(item.id, item.Clone() as Item);
                                }
                            }
                            else if (result is Appliance)
                            {
                                Appliance appliance = result as Appliance;
                                if (!Player.appliances.ContainsKey(appliance.id))
                                {
                                    if (Player.appliances[applianceID] is IUpgradable)
                                    {
                                        IUpgradable target = Player.appliances[applianceID] as IUpgradable;
                                        if (target.UpgradeCheck(appliance))
                                        {
                                            Player.appliances.Remove(Player.appliances[applianceID].id);
                                            Appliance upgraded = target.Upgrade() as Appliance;
                                            Player.appliances.Add(upgraded.id, upgraded);
                                        }
                                        else
                                        {
                                            Player.appliances.Add(appliance.id, appliance);
                                        }
                                    }
                                    else
                                    {
                                        Player.appliances.Add(appliance.id, appliance);
                                    }
                                }
                            }
                        }
                        Dictionary<byte, object> parameter = new Dictionary<byte, object>
                                        {
                                            {(byte)ProduceResponseItem.ApplianceID,applianceID},
                                            {(byte)ProduceResponseItem.ProduceMethodID,produceMethodID}
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
    }
}
