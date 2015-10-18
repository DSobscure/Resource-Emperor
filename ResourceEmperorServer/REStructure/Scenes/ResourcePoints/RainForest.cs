using System.Collections.Generic;
using REProtocol;
using REStructure.Items.Materials;

namespace REStructure.Scenes.ResourcePoints
{
    class RainForest : ResourcePoint
    {
        protected RainForest() { }
        public RainForest(string name, List<Pathway> discoveredPaths)
        {
            this.name = name;
            this.discoveredPaths = discoveredPaths;
            collectionList = new Dictionary<CollectionMethod, Dictionary<Item, int>>()
            {
                { CollectionMethod.Hew, new Dictionary<Item, int>()
                    {
                        { new Log(1),3000 },
                        { new Oak(1),4000 }
                    }
                },
                { CollectionMethod.Fill, new Dictionary<Item, int>()
                    {
                        { new RawRubber(1),4000 }
                    }
                }
            };
        }
    }
}
