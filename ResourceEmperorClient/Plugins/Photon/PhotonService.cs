using System.Collections.Generic;
using ExitGames.Client.Photon;
using System;
using REProtocol;
using REStructure.Items.Materials;
using REStructure;
using RESerializable;

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
            this.peer = new PhotonPeer(this, ConnectionProtocol.Udp);
            if (!this.peer.Connect(serverAddress, serverNmae))
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
                this.peer.Disconnect();
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
            if (this.peer != null)
                this.peer.Service();
        }
        catch (Exception EX)
        {
            throw EX;
        }
    }


    public void DebugReturn(DebugLevel level, string message)
    {
        this.DebugMessage = message;
    }

    public void OnEvent(EventData eventData)
    {
        //switch (eventData.Code)
        //{
        //}
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
        }
    }

    public void OnStatusChanged(StatusCode statusCode)
    {
        switch (statusCode)
        {
            case StatusCode.Connect:
                this.peer.EstablishEncryption();
                break;
            case StatusCode.Disconnect:
                this.peer = null;
                this.ServerConnected = false;
                ConnectEvent(false);
                break;
            case StatusCode.EncryptionEstablished:
                this.ServerConnected = true;
                ConnectEvent(true);
                break;
        }
    }

    //OperationResponse Task
    private void LoginTask(OperationResponse operationResponse)
    {
        if (operationResponse.ReturnCode == (short)ErrorType.Correct)
        {
            LoginEvent(
                loginStatus: true,
                debugMessage: "",
                player: ((Dictionary<string, object>)operationResponse.Parameters[(byte)LoginResponseItem.PlayerDataString]).Deserialize<SerializablePlayer>(),
                inventoryDataString: (string)operationResponse.Parameters[(byte)LoginResponseItem.InventoryDataString]);
        }
        else
        {
            DebugReturn(0, operationResponse.DebugMessage);
            LoginEvent(false, operationResponse.DebugMessage,null,"");
        }
    }


    //Event Task

    //內部函數區塊   主動行為
    public void Test()
    {
        //Inventory inventory = new Inventory();
        //inventory.Add(ItemID.Bamboo, new Bamboo(ItemID.Bamboo, "海水", "desc", 20));
        //inventory.Add(ItemID.Clay, new Clay(ItemID.Clay, "海水", "desc", 1));
        //inventory.Add(ItemID.Cotton, new Cotton(ItemID.Cotton, "海水", "desc", 1));
        //inventory.Add(ItemID.IronOre, new IronOre(ItemID.IronOre, "海水", "desc", 7));
        //inventory.Add(ItemID.Rock, new Rock(ItemID.Rock, "海水", "desc", 1));
        //inventory.Add(ItemID.Water, new Water(ItemID.Water, "海水", "desc", 30));
        //var parameter = new Dictionary<byte, object> {
        //                     { 0, inventory.Serialize() }
        //                };
        //this.peer.OpCustom((byte)OperationType.Test, parameter, true, 0, true);
    }
    public void Login(string account, string password)
    {
        try
        {
            var parameter = new Dictionary<byte, object> { 
                             { (byte)LoginParameterItem.Account, account },   
                             { (byte)LoginParameterItem.Password, password }
                        };

            this.peer.OpCustom((byte)OperationType.Login, parameter, true, 0, true);
        }
        catch (Exception EX)
        {
            throw EX;
        }
    }
}
