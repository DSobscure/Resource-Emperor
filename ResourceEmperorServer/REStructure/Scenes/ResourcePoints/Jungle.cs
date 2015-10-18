using System.Collections.Generic;
using REProtocol;
using REStructure.Items.Materials;

namespace REStructure.Scenes.ResourcePoints
{
    class Jungle : ResourcePoint
    {
        protected Jungle() { }
        public Jungle(List<Pathway> allPathways)
        {
            name = "叢森";
            this.allPathways = allPathways;
            collectionList = new Dictionary<CollectionMethod, Dictionary<Item, int>>()
            {
                { CollectionMethod.Hew, new Dictionary<Item, int>()
                    {
                        { new Log(1), 7000 }
                    }
                }
            };
        }
    }
}
