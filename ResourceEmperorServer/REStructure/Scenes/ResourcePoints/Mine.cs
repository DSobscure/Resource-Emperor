using System.Collections.Generic;
using REProtocol;
using REStructure.Items.Materials;

namespace REStructure.Scenes.ResourcePoints
{
    public class Mine : ResourcePoint
    {
        protected Mine() { }
        public Mine(string name, List<Pathway> discoveredPaths)
        {
            this.name = name;
            this.discoveredPaths = discoveredPaths;
            collectionList = new Dictionary<CollectionMethod, Dictionary<Item, int>>()
            {
                { CollectionMethod.Dig, new Dictionary<Item, int>()
                    {
                        { new Rock(1), 2000 },
                        { new Coal(1), 4000 },
                        { new CopperOre(1), 1500 },
                        { new IronOre(1), 500 }
                    }
                }
            };
        }
    }
}
