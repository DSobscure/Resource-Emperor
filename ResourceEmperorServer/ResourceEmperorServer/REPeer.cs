using System;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using ExitGames.Logging;
using REProtocol;
using REStructure;
using System.Threading;
using REStructure.Scenes;

namespace ResourceEmperorServer
{
    public partial class REPeer : PeerBase
    {
        internal Guid guid;
        private REServer server;
        internal REPlayer player;
        private CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        private Room workRoom;

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
                    REServer.Log.Info(player.account + ": Disconnet");
                }
            }
            else
            {
                REServer.Log.Info(guid+": Disconnet");
            }
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            if(player == null)
                REServer.Log.Info(guid + ":"+((OperationType)operationRequest.OperationCode).ToString());
            else
                REServer.Log.Info(player.account + ":" + ((OperationType)operationRequest.OperationCode).ToString());
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

                #region go to scene
                case (byte)OperationType.GoToScene:
                    {
                        GoToSceneTask(operationRequest);
                    }
                    break;
                #endregion

                #region walk path
                case (byte)OperationType.WalkPath:
                    {
                        WalkPathTask(operationRequest);
                    }
                    break;
                #endregion

                #region explore
                case (byte)OperationType.Explore:
                    {
                        ExploreTask(operationRequest);
                    }
                    break;
                #endregion

                #region collect material
                case (byte)OperationType.CollectMaterial:
                    {
                        CollectMaterialTask(operationRequest);
                    }
                    break;
                #endregion

                #region send message
                case (byte)OperationType.SendMessage:
                    {
                        SendMessageTask(operationRequest);
                    }
                    break;
                #endregion

                #region get ranking
                case (byte)OperationType.GetRanking:
                    {
                        GetRankingTask(operationRequest);
                    }
                    break;
                #endregion

                #region get ranking
                case (byte)OperationType.LeaveMessage:
                    {
                        LeaveMessageTask(operationRequest);
                    }
                    break;
                #endregion
            }
        }
    }
}
