using System;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using ExitGames.Logging;
using REProtocol;
using REStructure;
using System.Threading;

namespace ResourceEmperorServer
{
    public partial class REPeer : PeerBase
    {
        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();
        public Guid guid { get; set; }
        private REServer server;
        public REPlayer Player { get; set; }
        private CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

        public REPeer(IRpcProtocol rpcprotocol, IPhotonPeer nativePeer, REServer serverApplication) : base(rpcprotocol,nativePeer)
        {
            guid = Guid.NewGuid();
            server = serverApplication;
            server.WandererDictionary.Add(guid, this);
        }

        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            if (!DisconnectAsWanderer())
            {
                if (!DisconnectAsPlayer())
                {
                    REServer.Log.Info("Disconnect Error because we don't know what is the target");
                }
                else
                {
                    REServer.Log.Info(Player.account + ": Disconnet");
                }
            }
            else
            {
                REServer.Log.Info(guid+": Disconnet");
            }
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            if(Player == null)
                REServer.Log.Info(guid + ":"+((OperationType)operationRequest.OperationCode).ToString());
            else
                REServer.Log.Info(Player.account + ":" + ((OperationType)operationRequest.OperationCode).ToString());
            switch (operationRequest.OperationCode)
            {
                #region test
                case (byte)OperationType.Test:
                    {
                        TestTask(operationRequest);
                    }
                    break;
                #endregion

                #region login
                case (byte)OperationType.Login:
                    {
                        LoginTask(operationRequest);
                    }
                    break;
                #endregion

                #region produce
                case (byte)OperationType.Produce:
                    {
                        ProduceTask(operationRequest);
                    }
                    break;
                #endregion

                #region cancel produce
                case (byte)OperationType.CancelProduce:
                    {
                        CancelProduceTask();
                    }
                    break;
                #endregion

                #region discard item
                case (byte)OperationType.DiscardItem:
                    {
                        DiscardItemTask(operationRequest);
                    }
                    break;
                #endregion
            }
        }
    }
}
