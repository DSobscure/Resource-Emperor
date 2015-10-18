using System.Collections.Generic;
using REProtocol;
using REStructure.Items.Materials;

namespace REStructure.Scenes.ResourcePoints
{
    public class Lode : ResourcePoint
    {
        protected Lode() { }
        public Lode(List<Pathway> allPathways)
        {
            name = "礦脈";
            this.allPathways = allPathways;
            collectionList = new Dictionary<CollectionMethod, Dictionary<Item, int>>()
            {
                { CollectionMethod.Dig, new Dictionary<Item, int>()
                    {
                        { new Coal(1), 2000 },
                        { new CopperOre(1), 5000 },
                        { new IronOre(1), 3000 }
                    }
                }
            };
        }
    }
}
