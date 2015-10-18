using System;
using System.Collections.Generic;
using System.IO;
using Photon.SocketServer;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using log4net.Config;
using REProtocol;

namespace ResourceEmperorServer
{
    public class REServer : ApplicationBase
    {
        public static readonly ILogger Log = LogManager.GetCurrentClassLogger();
        public Dictionary<Guid, REPeer> WandererDictionary;
        public Dictionary<int, REPlayer> PlayerDictionary;
        public REDatabase database;
        public Map map;

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

            WandererDictionary = new Dictionary<Guid, REPeer>();
            PlayerDictionary = new Dictionary<int, REPlayer>();

            Log.Info("Server Setup successiful!.......");

            database = new REDatabase("localhost", "root", "", "resource emperor");
            if (database.Connect())
            {
                Log.Info("Database Connect successiful!.......");
            }
        }

        protected override void TearDown()
        {
            database.Dispose();
        }

        public void Broadcast(REPeer[] peers, BroadcastType broadcastType, Dictionary<byte, object> parameter)
        {
            EventData eventData = new EventData((byte)broadcastType, parameter);
            foreach (REPeer peer in peers)
            {
                peer.SendEvent(eventData, new SendParameters());
            }
        }
    }
}
