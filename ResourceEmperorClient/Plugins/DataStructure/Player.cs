using REStructure;

public class ClientPlayer
{
    public int uniqueID { get; protected set; }
    public string account { get; protected set; }
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
            SelectItemEvent();
        }
    }

    public delegate void SelectItemEventHandler();
    public event SelectItemEventHandler SelectItemEvent;

    public ClientPlayer(Player player)
    {
        this.uniqueID = player.uniqueID;
        this.account = player.account;
        IsWorking = false;
    }
}
