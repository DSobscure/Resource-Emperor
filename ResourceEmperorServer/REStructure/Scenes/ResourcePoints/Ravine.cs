using System.Collections.Generic;
using REProtocol;
using REStructure.Items.Materials;

namespace REStructure.Scenes.ResourcePoints
{
    public class Ravine : ResourcePoint
    {
        protected Ravine() { }
        public Ravine(string name, List<Pathway> discoveredPaths)
        {
            this.name = name;
            this.discoveredPaths = discoveredPaths;
            collectionList = new Dictionary<CollectionMethod, Dictionary<Item, int>>()
            {
                { CollectionMethod.Fill, new Dictionary<Item, int>()
                    {
                        { new Water(1),9000 }
                    }
                },
                { CollectionMethod.Take, new Dictionary<Item,int>()
                    {
                        { new Rock(1),9000 }
                    }
                }
            };
        }
    }
}
