using System.Collections.Generic;
using REProtocol;
using Newtonsoft.Json;

namespace REStructure.Scenes
{
    public class Wilderness : Scene
    {
        [JsonProperty("allPathways")]
        public List<Pathway> discoveredPaths { get; protected set; }

        protected Wilderness() { }
        public Wilderness(string name, List<Pathway> discoveredPaths) : base(name)
        {
            this.discoveredPaths = discoveredPaths;
        }
    }
}
