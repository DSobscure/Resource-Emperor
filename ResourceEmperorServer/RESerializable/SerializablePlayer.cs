using Newtonsoft.Json;

namespace RESerializable
{
    public class SerializablePlayer
    {
        [JsonProperty("uniqueID")]
        public int uniqueID;
        [JsonProperty("account")]
        public string account;

        public SerializablePlayer() { }
        public SerializablePlayer(int uniqueID, string account)
        {
            this.uniqueID = uniqueID;
            this.account = account;
        }
    }
}
