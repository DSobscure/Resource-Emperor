using System.Collections.Generic;
using ExitGames.Client.Photon;
using System;
using REProtocol;
using REStructure;
using Newtonsoft.Json;

public partial class PhotonService : IPhotonPeerListener
{
    public PhotonPeer peer { get; protected set; }
    public bool ServerConnected { get; protected set; }
    public string DebugMessage { get; protected set; }

    public PhotonService()
    {
        peer = null;
        ServerConnected = false;
        DebugMessage = "";
    }

    public void Connect(string ip, int port, string serverNmae)
    {
        try
        {
            string serverAddress = ip + ":" + port.ToString();
            peer = new PhotonPeer(this, ConnectionProtocol.Udp);
            if (!peer.Connect(serverAddress, serverNmae))
            {
                ConnectEvent(false);
            }
        }
        catch (Exception EX)
        {
            ConnectEvent(false);
            throw EX;
        }
    }

    public void Disconnect()
    {
        try
        {
            if (peer != null)
                peer.Disconnect();
        }
        catch (Exception EX)
        {
            throw EX;
        }
    }

    public void Service()
    {
        try
        {
            if (peer != null)
                peer.Service();
        }
        catch (Exception EX)
        {
            throw EX;
        }
    }


    public void DebugReturn(DebugLevel level, string message)
    {
        DebugMessage = message;
    }

    public void OnEvent(EventData eventData)
    {
        switch (eventData.Code)
        {
            #region send message
            case (byte)BroadcastType.SendMessage:
                {
                    SendMessageEventTask(eventData);
                }
                break;
            #endregion
        }
    }

    public void OnOperationResponse(OperationResponse operationResponse)
    {
        switch (operationResponse.OperationCode)
        {
            #region login
            case (byte)OperationType.Login:
                {
                    LoginTask(operationResponse);
                }
                break;
            #endregion

            #region produce
            case (byte)OperationType.Produce:
                {
                    ProduceTask(operationResponse);
                }
                break;
            #endregion

            #region produce
            case (byte)OperationType.DiscardItem:
                {
                    DiscardItemTask(operationResponse);
                }
                break;
            #endregion

            #region go to scene
            case (byte)OperationType.GoToScene:
                {
                    GoToSceneTask(operationResponse);
                }
                break;
            #endregion

            #region walk path
            case (byte)OperationType.WalkPath:
                {
                    WalkPathTask(operationResponse);
                }
                break;
            #endregion

            #region explore
            case (byte)OperationType.Explore:
                {
                    ExploreTask(operationResponse);
                }
                break;
            #endregion

            #region collect material
            case (byte)OperationType.CollectMaterial:
                {
                    CollectMaterialTask(operationResponse);
                }
                break;
            #endregion

            #region send message
            case (byte)OperationType.SendMessage:
                {
                    SendMessageTask(operationResponse);
                }
                break;
            #endregion

            #region get ranking
            case (byte)OperationType.GetRanking:
                {
                    GetRankingTask(operationResponse);
                }
                break;
            #endregion
        }
    }

    public void OnStatusChanged(StatusCode statusCode)
    {
        switch (statusCode)
        {
            case StatusCode.Connect:
                peer.EstablishEncryption();
                break;
            case StatusCode.Disconnect:
                peer = null;
                ServerConnected = false;
                ConnectEvent(false);
                break;
            case StatusCode.EncryptionEstablished:
                ServerConnected = true;
                ConnectEvent(true);
                break;
        }
    }
}
