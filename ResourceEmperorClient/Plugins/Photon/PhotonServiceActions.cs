using System.Collections.Generic;
using ExitGames.Client.Photon;
using System;
using REProtocol;
using REStructure;
using REStructure.Items;
using Newtonsoft.Json;

public partial class PhotonService : IPhotonPeerListener
{
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
    public void Produce(Appliance appliance, ProduceMethod method)
    {
        try
        {
            var parameter = new Dictionary<byte, object> {
                             { (byte)ProduceParameterItem.ApplianceID, appliance.id },
                             { (byte)ProduceParameterItem.ProduceMethodID, method.id }
                        };

            this.peer.OpCustom((byte)OperationType.Produce, parameter, true, 0, true);
        }
        catch (Exception EX)
        {
            throw EX;
        }
    }
    public void CancelProduce()
    {
        try
        {
            var parameter = new Dictionary<byte, object>();

            this.peer.OpCustom((byte)OperationType.CancelProduce, parameter, true, 0, true);
        }
        catch (Exception EX)
        {
            throw EX;
        }
    }
    public void DiscardItem(ItemID discardItemID, int discardCount)
    {
        try
        {
            var parameter = new Dictionary<byte, object> {
                             { (byte)DiscardItemParameterItem.ItemID, discardItemID},
                             { (byte)DiscardItemParameterItem.DiscardCount, discardCount }
                        };

            this.peer.OpCustom((byte)OperationType.DiscardItem, parameter, true, 0, true);
        }
        catch (Exception EX)
        {
            throw EX;
        }
    }
    public void GoToScene(int TargetSceneID)
    {
        try
        {
            var parameter = new Dictionary<byte, object> {
                             { (byte)GoToSceneParameterItem.TargetSceneID, TargetSceneID}
                        };

            this.peer.OpCustom((byte)OperationType.GoToScene, parameter, true, 0, true);
        }
        catch (Exception EX)
        {
            throw EX;
        }
    }
    public void WalkPath(int PathwayID)
    {
        try
        {
            var parameter = new Dictionary<byte, object> {
                             { (byte)WalkPathParameterItem.PathwayID, PathwayID}
                        };

            this.peer.OpCustom((byte)OperationType.WalkPath, parameter, true, 0, true);
        }
        catch (Exception EX)
        {
            throw EX;
        }
    }
    public void Explore()
    {
        try
        {
            this.peer.OpCustom((byte)OperationType.Explore, new Dictionary<byte, object>(), true, 0, true);
        }
        catch (Exception EX)
        {
            throw EX;
        }
    }
    public void CollectMaterial(CollectionMethod method, Tool tool)
    {
        try
        {
            var parameter = new Dictionary<byte, object> {
                             { (byte)CollectMaterialParameterItem.CollectiontMethod, method},
                             { (byte)CollectMaterialParameterItem.ToolID, (tool == null) ? ItemID.No : tool.id}
                        };

            this.peer.OpCustom((byte)OperationType.CollectMaterial, parameter, true, 0, true);
        }
        catch (Exception EX)
        {
            throw EX;
        }
    }
}
