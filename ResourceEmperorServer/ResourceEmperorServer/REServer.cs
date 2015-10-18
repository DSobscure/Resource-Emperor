using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Photon.SocketServer;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using log4net.Config;
using REStructure;
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
            //map = new Map(new List<ServerScene>()
            //{
            //    new ServerTown("起始村莊", new List<Scene>()
            //        {
            //            new ServerWilderness("邊緣地帶",10000,new List<Scene>()
            //                {
            //                    new ServerWilderness("往海邊的道路",9000,new List<Scene>()
            //                        {
            //                            new ServerWilderness("矮樹叢",6000,new List<Scene>()
            //                                {
            //                                    new ServerResourcePoint("茂密的森林",5000,new List<Scene>(),
            //                                        new Dictionary<CollectionMethod, Dictionary<ItemID, int>>()
            //                                        {
            //                                        }
            //                                    ),
            //                                    new ServerResourcePoint("叢森",4000,new List<Scene>()
            //                                        {
            //                                            new ServerResourcePoint("雨林",3000,new List<Scene>()
            //                                                {

            //                                                },
            //                                                new Dictionary<CollectionMethod, Dictionary<ItemID, int>>()
            //                                                {

            //                                                }
            //                                            ),
            //                                            new ServerResourcePoint("溪流",6000,new List<Scene>()
            //                                                {

            //                                                },
            //                                                new Dictionary<CollectionMethod, Dictionary<ItemID, int>>()
            //                                                {

            //                                                }
            //                                            )
            //                                        },
            //                                        new Dictionary<CollectionMethod, Dictionary<ItemID, int>>()
            //                                        {

            //                                        }
            //                                    )
            //                                }
            //                            ),
            //                            new ServerResourcePoint("灌木叢",4000,new List<Scene>()
            //                                {
            //                                    new ServerResourcePoint("更深的灌木叢",1000,new List<Scene>()
            //                                        {

            //                                        },
            //                                        new Dictionary<CollectionMethod, Dictionary<ItemID, int>>()
            //                                        {

            //                                        }
            //                                    )
            //                                },
            //                                new Dictionary<CollectionMethod, Dictionary<ItemID, int>>()
            //                                {

            //                                }
            //                            ),
            //                            new ServerWilderness("防風林",8000,new List<Scene>()
            //                                {
            //                                    new ServerResourcePoint("海岸樹叢",9000,new List<Scene>()
            //                                        {

            //                                        },
            //                                        new Dictionary<CollectionMethod, Dictionary<ItemID, int>>()
            //                                        {

            //                                        }
            //                                    ),
            //                                    new ServerResourcePoint("海灘",10000,new List<Scene>()
            //                                        {

            //                                        },
            //                                        new Dictionary<CollectionMethod, Dictionary<ItemID, int>>()
            //                                        {
            //                                        }
            //                                    )
            //                                }
            //                            )
            //                        }
            //                    ),
            //                    new ServerWilderness("往森林的道路",9000,new List<Scene>()
            //                        {
                                        
            //                        }
            //                    )
            //                }
            //            )
            //        }
            //    )
            //});
            Log.Info("Map Create successiful!.......");
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
