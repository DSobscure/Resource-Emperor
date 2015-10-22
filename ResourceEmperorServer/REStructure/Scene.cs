using System.Collections.Generic;
using REProtocol;
using Newtonsoft.Json;
using System;

namespace REStructure
{
    public abstract class Scene
    {
        public int  uniqueID { get; protected set; }
        [JsonProperty("name")]
        public string name { get; protected set; }
        public List<Player> players { get; protected set; }
        public List<Pathway> paths { get; protected set; }
        private static int sceneCount = 0;

        protected Scene()
        {
            uniqueID = sceneCount++;
            players = new List<Player>();
            paths = new List<Pathway>();
        }
        protected Scene(string name)
        {
            uniqueID = sceneCount++;
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
