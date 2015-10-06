using System;
using Newtonsoft.Json;

namespace RESerializable
{
    public class SerializableScene
    {
        [JsonProperty("uniqueID")]
        public int uniqueID;
        [JsonProperty("name")]
        public string name;

        public SerializableScene(int uniqueID, string name)
        {
            this.uniqueID = uniqueID;
            this.name = name;
        }
    }
}
