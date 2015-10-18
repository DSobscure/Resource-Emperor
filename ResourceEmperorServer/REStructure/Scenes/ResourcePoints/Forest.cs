using System.Collections.Generic;
using REProtocol;
using REStructure.Items.Materials;

namespace REStructure.Scenes.ResourcePoints
{
    public class Forest : ResourcePoint
    {
        protected Forest() { }
        public Forest(List<Pathway> allPathways)
        {
            name = "森林";
            this.allPathways = allPathways;
            collectionList = new Dictionary<CollectionMethod, Dictionary<Item, int>>()
            {
                { CollectionMethod.Hew, new Dictionary<Item, int>()
                    {
                        { new Log(1), 4000 },
                        { new Bamboo(1), 2000 }
                    }
                }
            };
        }
    }
}
