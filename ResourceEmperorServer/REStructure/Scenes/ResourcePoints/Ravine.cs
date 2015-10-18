using System.Collections.Generic;
using REProtocol;
using REStructure.Items.Materials;

namespace REStructure.Scenes.ResourcePoints
{
    public class Ravine : ResourcePoint
    {
        protected Ravine() { }
        public Ravine(List<Pathway> allPathways)
        {
            name = "溪谷";
            this.allPathways = allPathways;
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
