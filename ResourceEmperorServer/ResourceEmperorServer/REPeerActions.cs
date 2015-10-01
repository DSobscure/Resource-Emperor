using System;
using Photon.SocketServer;
using REStructure.Items.Materials;
using REStructure;
using REProtocol;

namespace ResourceEmperorServer
{
    public partial class REPeer : PeerBase
    {
        private bool Login(int playerUniqueID)
        {
            if (server.WandererDictionary.ContainsKey(guid))
            {
                string[] requestItem = new string[1];
                requestItem[0] = "Account";

                TypeCode[] requestType = new TypeCode[1];
                requestType[0] = TypeCode.String;

                object[] returnData = server.database.GetDataByUniqueID(playerUniqueID, requestItem, requestType, "player");

                Player = new REPlayer(playerUniqueID, (string)returnData[0], this);
                Player.inventory = new Inventory();
                Player.inventory.Add(ItemID.Bamboo, new Bamboo(20));
                Player.inventory.Add(ItemID.Clay, new Clay(1));
                Player.inventory.Add(ItemID.Cotton, new Cotton(1));
                Player.inventory.Add(ItemID.IronOre, new IronOre(7));
                Player.inventory.Add(ItemID.Rock, new Rock(1));
                Player.inventory.Add(ItemID.Water, new Water(30));

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
                server.PlayerDictionary.Remove(Player.uniqueID);
                //Dictionary<byte, object> parameter = new Dictionary<byte, object>
                //                        {
                //                            {(byte)DisconnectBroadcastItem.SoulUniqueIDListDataString,SerializeFunction.SerializeObject(soulUniqueIDList.ToArray())},
                //                            {(byte)DisconnectBroadcastItem.SceneUniqueIDListDataString,SerializeFunction.SerializeObject(sceneUniqueIDList.ToArray())},
                //                            {(byte)DisconnectBroadcastItem.ContainerUniqueIDListDataString,SerializeFunction.SerializeObject(containerUniqueIDList.ToArray())}
                //                        };
                //HashSet<DSPeer> peers = new HashSet<DSPeer>();
                //foreach (Answer answer in server.AnswerDictionary.Values)
                //{
                //    peers.Add(answer.Peer);
                //}
                //foreach (Scene scene in server.SceneAdministratorDictionary.Values)
                //{
                //    peers.Add(scene.AdministratorPeer);
                //}
                //server.Broadcast(peers.ToArray(), BroadcastType.Disconnect, parameter);

                //string[] updateItems = { "PositionX", "PositionY", "PositionZ", "EulerAngleY" };
                //object[] updateValues = { container.PositionX, container.PositionY, container.PositionZ, container.EulerAngleY };
                //string table = "container";
                //server.database.UpdateDataByUniqueID(containerUniqueID, updateItems, updateValues, table);

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
