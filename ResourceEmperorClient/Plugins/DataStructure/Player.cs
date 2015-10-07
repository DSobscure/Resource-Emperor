using RESerializable;
using REStructure;

public class Player
{
    public int uniqueID { get; protected set; }
    public string account { get; protected set; }
    public bool isWorking { get; set; }

    public Player(SerializablePlayer player)
    {
        this.uniqueID = player.uniqueID;
        this.account = player.account;
        isWorking = false;
    }
}
