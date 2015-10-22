using REStructure;

public class ClientPlayer
{
    public int uniqueID { get; protected set; }
    public string account { get; protected set; }
    public bool IsWorking { get; set; }
    public Scene Location { get; set; }

    public ClientPlayer(Player player)
    {
        this.uniqueID = player.uniqueID;
        this.account = player.account;
        IsWorking = false;
    }
}
