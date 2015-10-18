using System.Collections.Generic;
using REProtocol;
using Newtonsoft.Json;

namespace REStructure
{
    public abstract class Scene
    {
        [JsonProperty("name")]
        public string name { get; protected set; }
        public List<Player> players { get; protected set; }
        public List<Pathway> paths { get; protected set; }

        protected Scene() { }
        protected Scene(string name)
        {
            this.name = name;
            players = new List<Player>();
            paths = new List<Pathway>();
        }

        public void AddPath(Pathway path)
        {
            paths.Add(path);
        }
    }
}
