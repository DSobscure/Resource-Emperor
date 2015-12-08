using REStructure;

public class ClientPlayer : Player
{
    public bool IsWorking { get; set; }
    public Scene Location { get; set; }

    private Item _selectedItem;
    public Item SelectedItem
    {
        get
        {
            return _selectedItem;
        }
        set
        {
            _selectedItem = value;
            if(SelectItemEvent != null)
                SelectItemEvent();
        }
    }

    public delegate void SelectItemEventHandler();
    public event SelectItemEventHandler SelectItemEvent;

    public ClientPlayer(Player player)
    {
        uniqueID = player.uniqueID;
        account = player.account;
        IsWorking = false;
        money = player.money;
    }
}
