﻿namespace REProtocol
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
}
