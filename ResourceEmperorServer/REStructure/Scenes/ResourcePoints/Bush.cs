using System.Collections.Generic;
using REProtocol;
using REStructure.Items.Materials;

namespace REStructure.Scenes.ResourcePoints
{
    public class Bush : ResourcePoint
    {
        protected Bush() { }
        public Bush(List<Pathway> allPathways)
        {
            name = "灌木叢";
            this.allPathways = allPathways;
            collectionList = new Dictionary<CollectionMethod, Dictionary<Item, int>>()
            {
                { CollectionMethod.Take, new Dictionary<Item, int>()
                    {
                        { new Cotton(1), 4000 }
                    }
                }
            };
        }
    }
}
