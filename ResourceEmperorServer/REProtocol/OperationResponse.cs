namespace REProtocol
{
    public enum LoginResponseItem
    {
        PlayerDataString,
        InventoryDataString,
        AppliancesDataString
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
}
