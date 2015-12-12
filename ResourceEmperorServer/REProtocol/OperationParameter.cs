namespace REProtocol
{
    public enum LoginParameterItem
    {
        Account,
        Password,
        Version
    }

    public enum GetMaterialParameterItem
    {
        MaterialID,
        MaterialCount
    }

    public enum ProduceParameterItem
    {
        ApplianceID,
        ProduceMethodID
    }

    public enum DiscardItemParameterItem
    {
        ItemID,
        DiscardCount
    }

    public enum GoToSceneParameterItem
    {
        TargetSceneID
    }

    public enum WalkPathParameterItem
    {
        PathwayID
    }

    public enum CollectMaterialParameterItem
    {
        CollectiontMethod,
        ToolID
    }

    public enum SendMessageParameterItem
    {
        Message
    }

    public enum LeaveMessageParameterItem
    {
        Message
    }

    public enum TradeCommodityItem
    {
        IsPurchase,
        CommodityID,
        Count
    }
}
