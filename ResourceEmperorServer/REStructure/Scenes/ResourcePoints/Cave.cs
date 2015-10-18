using System.Collections.Generic;
using REProtocol;
using REStructure.Items.Materials;

namespace REStructure.Scenes.ResourcePoints
{
    public class Cave : ResourcePoint
    {
        protected Cave() { }
        public Cave(List<Pathway> allPathways)
        {
            name = "山洞";
            this.allPathways = allPathways;
            collectionList = new Dictionary<CollectionMethod, Dictionary<Item, int>>()
            {
                { CollectionMethod.Dig, new Dictionary<Item, int>()
                    {
                        { new Coal(1), 2000 },
                        { new Rock(1), 4000 }
                    }
                }
            };
        }
    }
}
