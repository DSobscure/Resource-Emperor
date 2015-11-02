using System;
using Photon.SocketServer;
using REStructure.Items.Materials;
using REStructure;
using REProtocol;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using REStructure.Scenes;

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
                    player = new REPlayer(playerUniqueID, (string)returnData[0], (string)returnData[1], (string)returnData[2], this);
                }
                else
                {
                    player = new REPlayer(playerUniqueID, (string)returnData[0], this);
                }
                workRoom = new Room("工作小屋");
                server.WandererDictionary.Remove(guid);
                server.PlayerDictionary.Add(playerUniqueID, player);
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
            if (player != null && server.PlayerDictionary.ContainsKey(player.uniqueID))
            {
                cancelTokenSource.Cancel();
                server.PlayerDictionary.Remove(player.uniqueID);
                string[] updateItems = { "Inventory", "Appliances" };
                object[] updateValues = {
                    JsonConvert.SerializeObject(player.inventory, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto }),
                    JsonConvert.SerializeObject(player.appliances, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto })
                };
                string table = "player";
                server.database.UpdateDataByUniqueID(player.uniqueID, updateItems, updateValues, table);
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
                await Task.Delay(player.appliances[applianceID].methods[produceMethodID].processTime * 1000, cancelTokenSource.Token);
                foreach (object result in results)
                {
                    if (result is Item)
                    {
                        Item item = result as Item;
                        if (player.inventory.ContainsKey(item.id))
                        {
                            player.inventory[item.id].Increase(item.itemCount);
                        }
                        else
                        {
                            player.inventory.Add(item.id, item.Clone() as Item);
                        }
                    }
                    else if (result is Appliance)
                    {
                        Appliance appliance = result as Appliance;
                        if (!player.appliances.ContainsKey(appliance.id))
                        {
                            if (player.appliances[applianceID] is IUpgradable)
                            {
                                IUpgradable target = player.appliances[applianceID] as IUpgradable;
                                if (target.UpgradeCheck(appliance))
                                {
                                    player.appliances.Remove(player.appliances[applianceID].id);
                                    Appliance upgraded = target.Upgrade() as Appliance;
                                    player.appliances.Add(upgraded.id, upgraded);
                                }
                                else
                                {
                                    player.appliances.Add(appliance.id, appliance);
                                }
                            }
                            else
                            {
                                player.appliances.Add(appliance.id, appliance);
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
        private bool SendMessage_and_Broadcast(string message)
        {
            Dictionary<byte, object> parameter = new Dictionary<byte, object>
                                        {
                                            {(byte)SendMessageBroadcastItem.SceneID, player.Location.uniqueID },
                                            {(byte)SendMessageBroadcastItem.PlayerName, player.account},
                                            {(byte)SendMessageBroadcastItem.Message,message}
                                        };
            List<REPeer> peers = new List<REPeer>();
            foreach(REPlayer player in player.Location.players)
            {
                peers.Add(player.peer);
            }
            server.Broadcast(peers.ToArray(), BroadcastType.SendMessage, parameter);
            return true;
        }
    }
}
