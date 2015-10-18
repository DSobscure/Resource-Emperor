using System.Collections.Generic;
using REProtocol;
using REStructure.Items.Materials;

namespace REStructure.Scenes.ResourcePoints
{
    public class CostalGrove : ResourcePoint
    {
        protected CostalGrove() { }
        public CostalGrove(List<Pathway> allPathways)
        {
            name = "海岸樹叢";
            this.allPathways = allPathways;
            collectionList = new Dictionary<CollectionMethod, Dictionary<Item, int>>()
            {
                { CollectionMethod.Take, new Dictionary<Item, int>()
                    {
                        { new Coconut(1), 4000 }
                    }
                }
            };
        }
    }
}
