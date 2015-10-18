using Newtonsoft.Json;

namespace REStructure
{
    public class Player
    {
        [JsonProperty("uniqueID")]
        public int uniqueID { get; protected set; }
        [JsonProperty("account")]
        public string account { get; protected set; }

        public Player() { }
        public Player(int uniqueID, string account)
        {
            this.uniqueID = uniqueID;
            this.account = account;
        }
    }
}
