using System.Collections.Generic;
using REProtocol;
using REStructure.Items.Materials;

namespace REStructure.Scenes.ResourcePoints
{
    public class Grassland : ResourcePoint
    {
        protected Grassland() { }
        public Grassland(List<Pathway> allPathways)
        {
            name = "草原";
            this.allPathways = allPathways;
            collectionList = new Dictionary<CollectionMethod, Dictionary<Item, int>>()
            {
                { CollectionMethod.Take, new Dictionary<Item, int>()
                    {
                        { new Hemp(1), 3000 },
                        { new Rock(1), 2000 }
                    }
                }
            };
        }
    }
}
