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
    private string debugMessage;

    public PhotonService()
    {
        peer = null;
        ServerConnected = false;
    }

    public void Connect(string ip, int port, string serverNmae)
    {
        try
        {
            string serverAddress = ip + ":" + port.ToString();
            peer = new PhotonPeer(this, ConnectionProtocol.Udp);
            if (!peer.Connect(serverAddress, serverNmae))
            {
                OnConnectResponse(false);
            }
        }
        catch (Exception ex)
        {
            OnConnectResponse(false);
            DebugReturn(DebugLevel.ERROR, ex.Message);
            DebugReturn(DebugLevel.ERROR, ex.StackTrace);
        }
    }

    public void Disconnect()
    {
        try
        {
            peer.Disconnect();
        }
        catch (Exception ex)
        {
            DebugReturn(DebugLevel.ERROR, ex.Message);
            DebugReturn(DebugLevel.ERROR, ex.StackTrace);
        }
    }

    public void Service()
    {
        try
        {
            peer.Service();
        }
        catch (Exception ex)
        {
            DebugReturn(DebugLevel.ERROR, ex.Message);
            DebugReturn(DebugLevel.ERROR, ex.StackTrace);
        }
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

            #region market change
            case (byte)BroadcastType.MarketChange:
                {
                    MarketChangeEventTask(eventData);
                }
                break;
            #endregion
        }
    }

    public void OnOperationResponse(OperationResponse operationResponse)
    {
        switch (operationResponse.OperationCode)
        {
            case (byte)OperationType.Login:
                LoginResponseTask(operationResponse);
                break;

            case (byte)OperationType.Produce:
                ProduceResponseTask(operationResponse);
                break;

            case (byte)OperationType.DiscardItem:
                DiscardItemResponseTask(operationResponse);
                break;

            case (byte)OperationType.GoToScene:
                GoToSceneResponseTask(operationResponse);
                break;

            case (byte)OperationType.WalkPath:
                WalkPathResponseTask(operationResponse);
                break;

            case (byte)OperationType.Explore:
                ExploreResponseTask(operationResponse);
                break;

            case (byte)OperationType.CollectMaterial:
                CollectMaterialResponseTask(operationResponse);
                break;

            case (byte)OperationType.SendMessage:
                SendMessageResponseTask(operationResponse);
                break;

            case (byte)OperationType.GetRanking:
                GetRankingResponseTask(operationResponse);
                break;

            case (byte)OperationType.LeaveMessage:
                LeaveMessageResponseTask(operationResponse);
                break;

            case (byte)OperationType.TradeCommodity:
                TradeCommodityResponseTask(operationResponse);
                break;

            case (byte)OperationType.GetMarket:
                GetMarketResponseTask(operationResponse);
                break;
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
                OnConnectResponse(false);
                break;
            case StatusCode.EncryptionEstablished:
                ServerConnected = true;
                OnConnectResponse(true);
                break;
        }
    }

    public void DebugReturn(DebugLevel level, string message)
    {
        if (OnDebugReturn != null)
            OnDebugReturn(message);
    }
}
