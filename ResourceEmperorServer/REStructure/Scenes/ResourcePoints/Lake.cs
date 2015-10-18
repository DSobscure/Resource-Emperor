using System.Collections.Generic;
using REProtocol;
using REStructure.Items.Materials;

namespace REStructure.Scenes.ResourcePoints
{
    public class Lake : ResourcePoint
    {
        protected Lake() { }
        public Lake(string name, List<Pathway> discoveredPaths)
        {
            this.name = name;
            this.discoveredPaths = discoveredPaths;
            collectionList = new Dictionary<CollectionMethod, Dictionary<Item, int>>()
            {
                { CollectionMethod.Fill, new Dictionary<Item, int>()
                    {
                        { new Water(1), 10000 }
                    }
                },
                { CollectionMethod.Take, new Dictionary<Item, int>()
                    {
                        { new Clay(1), 7000 },
                        { new Rock(1), 1000 }
                    }
                }
            };
        }
    }
}
