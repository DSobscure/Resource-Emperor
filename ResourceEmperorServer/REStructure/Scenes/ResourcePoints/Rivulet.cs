using System.Collections.Generic;
using REProtocol;
using REStructure.Items.Materials;

namespace REStructure.Scenes.ResourcePoints
{
    public class Rivulet : ResourcePoint
    {
        protected Rivulet() { }
        public Rivulet(string name, List<Pathway> discoveredPaths)
        {
            this.name = name;
            this.discoveredPaths = discoveredPaths;
            collectionList = new Dictionary<CollectionMethod, Dictionary<Item, int>>()
            {
                { CollectionMethod.Fill, new Dictionary<Item, int>()
                    {
                        { new Water(1),8500 }
                    }
                }
            };
        }
    }
}
