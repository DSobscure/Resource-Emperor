namespace REProtocol
{
    public enum LoginResponseItem
    {
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
        TargetSceneID,
        Messages
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
    public enum TradeCommodityResponseItem
    {
        InventoryDataString,
        Money
    }
    public enum GetMarketResponseItem
    {
        MarketDataString
    }
}
