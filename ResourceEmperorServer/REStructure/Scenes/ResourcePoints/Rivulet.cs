using System.Collections.Generic;
using REProtocol;
using REStructure.Items.Materials;

namespace REStructure.Scenes.ResourcePoints
{
    public class Rivulet : ResourcePoint
    {
        protected Rivulet() { }
        public Rivulet(List<Pathway> allPathways)
        {
            name = "溪流";
            this.allPathways = allPathways;
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
