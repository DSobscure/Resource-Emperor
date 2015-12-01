namespace REProtocol
{
    public enum LoginParameterItem
    {
        Account,
        Password
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
}
