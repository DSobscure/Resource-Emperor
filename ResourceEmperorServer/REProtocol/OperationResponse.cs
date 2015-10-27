namespace REProtocol
{
    public enum LoginResponseItem
    {
        PlayerDataString,
        InventoryDataString,
        AppliancesDataString,
        Version
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
}
