using System.Collections.Generic;
using REProtocol;
using REStructure.Items.Materials;

namespace REStructure.Scenes.ResourcePoints
{
    public class DeeperForest : ResourcePoint
    {
        protected DeeperForest() { }
        public DeeperForest(List<Pathway> allPathways)
        {
            name = "更深的森林";
            this.allPathways = allPathways;
            collectionList = new Dictionary<CollectionMethod, Dictionary<Item, int>>()
            {
                { CollectionMethod.Hew, new Dictionary<Item, int>()
                    {
                        { new Log(1), 7000 },
                        { new Bamboo(1), 1000 }
                    }
                }
            };
        }
    }
}
