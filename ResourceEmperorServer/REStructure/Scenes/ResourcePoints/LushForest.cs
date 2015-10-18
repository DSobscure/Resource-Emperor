using System.Collections.Generic;
using REProtocol;
using REStructure.Items.Materials;

namespace REStructure.Scenes.ResourcePoints
{
    public class LushForest : ResourcePoint
    {
        protected LushForest() { }
        public LushForest(List<Pathway> allPathways)
        {
            name = "茂密的森林";
            this.allPathways = allPathways;
            collectionList = new Dictionary<CollectionMethod, Dictionary<Item, int>>()
            {
                { CollectionMethod.Hew, new Dictionary<Item, int>()
                    {
                        { new Log(1), 4000 }
                    }
                }
            };
        }
    }
}
