using RESerializable;
using REStructure;

public class Player
{
    public int uniqueID { get; protected set; }
    public string account { get; protected set; }
    public Inventory inventory { get; protected set; }

    public Player(SerializablePlayer player)
    {
        this.uniqueID = player.uniqueID;
        this.account = player.account;
        inventory = new Inventory();
    }
}
