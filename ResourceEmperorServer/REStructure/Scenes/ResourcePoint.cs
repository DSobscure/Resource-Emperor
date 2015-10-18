using System.Collections.Generic;
using REProtocol;
using Newtonsoft.Json;

namespace REStructure.Scenes
{
    public class ResourcePoint : Wilderness
    {
        [JsonProperty("collectionList")]
        public Dictionary<CollectionMethod,Dictionary<Item,int>> collectionList { get; protected set; }

        protected ResourcePoint() { }
        public ResourcePoint(string name, List<Pathway> allPathways, Dictionary<CollectionMethod, Dictionary<Item, int>> collectionList)
            : base(name, allPathways)
        {
            this.collectionList = collectionList;
        }
    }
}
