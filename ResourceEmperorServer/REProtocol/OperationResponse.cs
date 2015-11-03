﻿namespace REProtocol
{
    public enum LoginResponseItem
    {
        Version,
        PlayerDataString,
        InventoryDataString,
        AppliancesDataString,
    }
    public enum ProduceResponseItem
    {
        ApplianceID,
        ProduceMethodID
    }
    public enum DiscardItemResponseItem
    {
        ItemID,
        ItemCount
    }
    public enum GoToSceneResponseItem
    {
        TargetSceneID
    }
    public enum WalkPathResponseItem
    {
        PathID,
        TargetSceneID
    }
    public enum ExploreResponseItem
    {
        DiscoveredPathIDsDataString
    }
    public enum CollectMaterialResponseItem
    {
        InventoryDataString
    }
    public enum GetRankingResponseItem
    {
        RankingDataString
    }
}
