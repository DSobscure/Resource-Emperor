using Newtonsoft.Json;
using Photon.SocketServer;
using REProtocol;
using REStructure;
using REStructure.Scenes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace ResourceEmperorServer
{
    public partial class REPeer : PeerBase
    {
        private bool Login(int playerUniqueID)
        {
            if (server.wandererDictionary.ContainsKey(guid))
            {
                string[] requestItem = new string[4];
                requestItem[0] = "Account";
                requestItem[1] = "Inventory";
                requestItem[2] = "Appliances";
                requestItem[3] = "Money";

                string[] returnData = server.database.GetDataByUniqueID(playerUniqueID, requestItem, "player");

                if (returnData[1] != "" && returnData[2] != "")
                {
                    player = new REPlayer(playerUniqueID, returnData[0], returnData[1], returnData[2], int.Parse(returnData[3]), this);
                }
                else
                {
                    player = new REPlayer(playerUniqueID, (string)returnData[0], this);
                }
                workRoom = new Room("工作小屋");
                server.PlayerOnline(player);
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool DisconnectAsWanderer()
        {
            if (server.wandererDictionary.ContainsKey(guid))
            {
                return server.wandererDictionary.Remove(guid);
            }
            else
            {
                return false;
            }
        }
        private bool DisconnectAsPlayer()
        {
            if (player != null && server.playerDictionary.ContainsKey(player.uniqueID))
            {
                cancelTokenSource.Cancel();
                server.PlayerOffline(player);
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
                        player.inventory.Stack(item);
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
                    DebugMessage = "工作已經消了"
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
        private void BroadcastMarketChange()
        {
            Dictionary<byte, object> parameter = new Dictionary<byte, object>
                                        {
                                            {(byte)MarketChangeBroadcastItem.MarketDataString, JsonConvert.SerializeObject((player.Location as Town).market, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto }) }
                                        };
            List<REPeer> peers = new List<REPeer>();
            foreach (REPlayer player in player.Location.players)
            {
                peers.Add(player.peer);
            }
            server.Broadcast(peers.ToArray(), BroadcastType.MarketChange, parameter);
        }
    }
}
