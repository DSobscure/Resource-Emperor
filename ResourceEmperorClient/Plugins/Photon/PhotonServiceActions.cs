using ExitGames.Client.Photon;
using REProtocol;
using REStructure;
using REStructure.Items;
using System;
using System.Collections.Generic;

public partial class PhotonService : IPhotonPeerListener
{
    public void Test()
    {
    }
    public void Login(string account, string password)
    {
        var parameter = new Dictionary<byte, object> {
                             { (byte)LoginParameterItem.Account, account },
                             { (byte)LoginParameterItem.Password, password },
                             { (byte)LoginParameterItem.Version, GameGlobal.version }
                        };

        peer.OpCustom((byte)OperationType.Login, parameter, true, 0, true);
    }
    public void Produce(Appliance appliance, ProduceMethod method)
    {
        GameGlobal.Player.IsWorking = true;
        var parameter = new Dictionary<byte, object> {
                             { (byte)ProduceParameterItem.ApplianceID, appliance.id },
                             { (byte)ProduceParameterItem.ProduceMethodID, method.id }
                        };

        peer.OpCustom((byte)OperationType.Produce, parameter, true, 0, true);
    }
    public void CancelProduce()
    {
        var parameter = new Dictionary<byte, object>();

        peer.OpCustom((byte)OperationType.CancelProduce, parameter, true, 0, true);
    }
    public void DiscardItem(ItemID discardItemID, int discardCount)
    {
        var parameter = new Dictionary<byte, object> {
                             { (byte)DiscardItemParameterItem.ItemID, discardItemID},
                             { (byte)DiscardItemParameterItem.DiscardCount, discardCount }
                        };

        peer.OpCustom((byte)OperationType.DiscardItem, parameter, true, 0, true);
    }
    public void GoToScene(int TargetSceneID)
    {
        var parameter = new Dictionary<byte, object> {
                             { (byte)GoToSceneParameterItem.TargetSceneID, TargetSceneID}
                        };

        peer.OpCustom((byte)OperationType.GoToScene, parameter, true, 0, true);
    }
    public void WalkPath(int PathwayID)
    {
        var parameter = new Dictionary<byte, object> {
                             { (byte)WalkPathParameterItem.PathwayID, PathwayID}
                        };

        peer.OpCustom((byte)OperationType.WalkPath, parameter, true, 0, true);
    }
    public void Explore()
    {
        peer.OpCustom((byte)OperationType.Explore, new Dictionary<byte, object>(), true, 0, true);
    }
    public void CollectMaterial(CollectionMethod method, Tool tool)
    {
        var parameter = new Dictionary<byte, object> {
                             { (byte)CollectMaterialParameterItem.CollectiontMethod, method},
                             { (byte)CollectMaterialParameterItem.ToolID, (tool == null) ? ItemID.No : tool.id}
                        };

        peer.OpCustom((byte)OperationType.CollectMaterial, parameter, true, 0, true);
    }
    public void SendMessage(string message)
    {
        var parameter = new Dictionary<byte, object> {
                             { (byte)SendMessageParameterItem.Message, message}
                        };

        peer.OpCustom((byte)OperationType.SendMessage, parameter, true, 0, true);
    }
    public void GetRanking()
    {
        peer.OpCustom((byte)OperationType.GetRanking, new Dictionary<byte, object>(), true, 0, true);
    }
    public void LeaveMessage(string message)
    {
        var parameter = new Dictionary<byte, object> {
                             { (byte)LeaveMessageParameterItem.Message, message}
                        };

        peer.OpCustom((byte)OperationType.LeaveMessage, parameter, true, 0, true);
    }
    public void PurchaseCommodity(ItemID commodityID, int count)
    {
        var parameter = new Dictionary<byte, object> {
                             { (byte)TradeCommodityItem.IsPurchase, true},
                             { (byte)TradeCommodityItem.CommodityID, (int)commodityID},
                             { (byte)TradeCommodityItem.Count, count}
                        };

        peer.OpCustom((byte)OperationType.TradeCommodity, parameter, true, 0, true);
    }
    public void SellCommodity(ItemID commodityID, int count)
    {
        var parameter = new Dictionary<byte, object> {
                             { (byte)TradeCommodityItem.IsPurchase, false},
                             { (byte)TradeCommodityItem.CommodityID, (int)commodityID},
                             { (byte)TradeCommodityItem.Count, count}
                        };

        peer.OpCustom((byte)OperationType.TradeCommodity, parameter, true, 0, true);
    }
    public void GetMarket()
    {
        var parameter = new Dictionary<byte, object>();
        peer.OpCustom((byte)OperationType.GetMarket, parameter, true, 0, true);
    }
}
