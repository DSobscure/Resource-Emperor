using System;
using System.Collections.Generic;
using System.IO;
using Photon.SocketServer;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using log4net.Config;
using REProtocol;
using REStructure;
using Newtonsoft.Json;
using System.Linq;

namespace ResourceEmperorServer
{
    public class REServer : ApplicationBase
    {
        public static readonly ILogger log = LogManager.GetCurrentClassLogger();
        public Dictionary<Guid, REPeer> wandererDictionary;
        public Dictionary<int, REPlayer> playerDictionary;
        public REDatabase database;
        public GlobalMap globalMap;
        public string version = "0.0.4";

        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            return new REPeer(initRequest.Protocol, initRequest.PhotonPeer, this);
        }

        protected override void Setup()
        {
            log4net.GlobalContext.Properties["Photon:ApplicationLogPath"] =
                Path.Combine(this.ApplicationPath, "log");

            string path = Path.Combine(this.BinaryPath, "log4net.config");
            var file = new FileInfo(path);
            if (file.Exists)
            {
                LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);
                XmlConfigurator.ConfigureAndWatch(file);
            }

            wandererDictionary = new Dictionary<Guid, REPeer>();
            playerDictionary = new Dictionary<int, REPlayer>();

            log.Info("Server Setup successiful!.......");

            database = new REDatabase("localhost", "root", "", "resource emperor");
            if (database.Connect())
            {
                log.Info("Database Connect successiful!.......");
            }

            globalMap = new GlobalMap();
            log.Info("Global Map Setup successiful!.......");
        }

        protected override void TearDown()
        {
            database?.Dispose();
        }

        public void Broadcast(REPeer[] peers, BroadcastType broadcastType, Dictionary<byte, object> parameter)
        {
            EventData eventData = new EventData((byte)broadcastType, parameter);
            foreach (REPeer peer in peers)
            {
                peer.SendEvent(eventData, new SendParameters());
            }
        }

        public bool GetRanking(out Dictionary<string, int> ranking)
        {
            if(database.GetRanking(out ranking))
            {
                foreach (var player in playerDictionary.Values)
                {
                    ranking[player.account] = player.money;
                }
                ranking = ranking.OrderBy(x=>x.Value).Reverse().ToDictionary(x => x.Key, x => x.Value); ;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void PlayerOnline(REPlayer player)
        {
            wandererDictionary.Remove(player.peer.guid);
            playerDictionary.Add(player.uniqueID, player);
        }

        public void PlayerOffline(REPlayer player)
        {
            playerDictionary.Remove(player.uniqueID);
            string[] updateItems = { "Inventory", "Appliances", "Money" };
            object[] updateValues = {
                    JsonConvert.SerializeObject(player.inventory, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto }),
                    JsonConvert.SerializeObject(player.appliances, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto }),
                    player.money
                };
            string table = "player";
            database.UpdateDataByUniqueID(player.uniqueID, updateItems, updateValues, table);
        }
    }
}
