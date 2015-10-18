using System.Collections.Generic;
using REProtocol;
using REStructure.Items.Materials;

namespace REStructure.Scenes.ResourcePoints
{
    public class DeeperMine : ResourcePoint
    {
        protected DeeperMine() { }
        public DeeperMine(string name, List<Pathway> discoveredPaths)
        {
            this.name = name;
            this.discoveredPaths = discoveredPaths;
            collectionList = new Dictionary<CollectionMethod, Dictionary<Item, int>>()
            {
                { CollectionMethod.Dig, new Dictionary<Item, int>()
                    {
                        { new Rock(1), 1000 },
                        { new Coal(1), 5000 },
                        { new CopperOre(1), 2000 },
                        { new IronOre(1), 1000 }
                    }
                }
            };
        }
    }
}
