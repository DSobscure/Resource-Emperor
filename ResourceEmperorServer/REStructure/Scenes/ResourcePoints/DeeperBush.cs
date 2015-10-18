using System.Collections.Generic;
using REProtocol;
using REStructure.Items.Materials;

namespace REStructure.Scenes.ResourcePoints
{
    public class DeeperBush : ResourcePoint
    {
        protected DeeperBush() { }
        public DeeperBush(List<Pathway> allPathways)
        {
            name = "更深的灌木叢";
            this.allPathways = allPathways;
            collectionList = new Dictionary<CollectionMethod, Dictionary<Item, int>>()
            {
                { CollectionMethod.Take, new Dictionary<Item, int>()
                    {
                        { new Cotton(1), 7000 }
                    }
                }
            };
        }
    }
}
