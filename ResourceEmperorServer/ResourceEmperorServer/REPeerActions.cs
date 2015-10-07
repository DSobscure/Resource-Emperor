using System;
using Photon.SocketServer;
using REStructure.Items.Materials;
using REStructure;
using REProtocol;
using Newtonsoft.Json;

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
    }
}
