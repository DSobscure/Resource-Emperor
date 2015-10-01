using System;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using ExitGames.Logging;
using REProtocol;
using REStructure;

namespace ResourceEmperorServer
{
    public partial class REPeer : PeerBase
    {
        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();
        public Guid guid { get; set; }
        private REServer server;
        public REPlayer Player { get; set; }

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
            }
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
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
            }
        }
    }
}
