using System;
using Photon.SocketServer;
using REStructure.Items.Materials;
using REStructure;
using REProtocol;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ResourceEmperorServer
{
    public partial class REPeer : PeerBase
    {
        private bool Login(int playerUniqueID)
        {
            if (server.WandererDictionary.ContainsKey(guid))
            {
                string[] requestItem = new string[3];
                requestItem[0] = "Account";
                requestItem[1] = "Inventory";
                requestItem[2] = "Appliances";

                string[] returnData = server.database.GetDataByUniqueID(playerUniqueID, requestItem, "player");

                if ((string)returnData[1] != "" && (string)returnData[2] != "")
                {
                    Player = new REPlayer(playerUniqueID, (string)returnData[0], (string)returnData[1], (string)returnData[2], this);
                }
                else
                {
                    Player = new REPlayer(playerUniqueID, (string)returnData[0], this);
                }

                server.WandererDictionary.Remove(guid);
                server.PlayerDictionary.Add(playerUniqueID, Player);
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool DisconnectAsWanderer()
        {
            if (server.WandererDictionary.ContainsKey(guid))
            {
                return server.WandererDictionary.Remove(guid);
            }
            else
            {
                return false;
            }
        }
        private bool DisconnectAsPlayer()
        {
            if (Player != null && server.PlayerDictionary.ContainsKey(Player.uniqueID))
            {
                cancelTokenSource.Cancel();
                server.PlayerDictionary.Remove(Player.uniqueID);
                string[] updateItems = { "Inventory", "Appliances" };
                object[] updateValues = {
                    JsonConvert.SerializeObject(Player.inventory, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto }),
                    JsonConvert.SerializeObject(Player.appliances, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto })
                };
                string table = "player";
                server.database.UpdateDataByUniqueID(Player.uniqueID, updateItems, updateValues, table);
                return true;
            }
            else
            {
                return false;
            }
        }
        async Task Produce(ApplianceID applianceID, ProduceMethodID produceMethodID, object[] results, byte operationCode)
        {
            try
            {
                await Task.Delay(Player.appliances[applianceID].methods[produceMethodID].processTime * 1000, cancelTokenSource.Token);
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

                OperationResponse response = new OperationResponse(operationCode, parameter)
                {
                    ReturnCode = (short)ErrorType.Correct,
                    DebugMessage = ""
                };
                SendOperationResponse(response, new SendParameters());
            }
            catch (TaskCanceledException ex)
            {
                OperationResponse response = new OperationResponse(operationCode)
                {
                    ReturnCode = (short)ErrorType.Canceled,
                    DebugMessage = ""
                };
                SendOperationResponse(response, new SendParameters());
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
