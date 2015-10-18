using System.Collections.Generic;
using REProtocol;
using REStructure.Items.Materials;

namespace REStructure.Scenes.ResourcePoints
{
    public class Beach : ResourcePoint
    {
        protected Beach() { }
        public Beach(List<Pathway> allPathways)
        {
            name = "海灘";
            this.allPathways = allPathways;
            collectionList = new Dictionary<CollectionMethod, Dictionary<Item, int>>()
            {
                { CollectionMethod.Fill, new Dictionary<Item, int>()
                    {
                        { new Brine(1),8000 },
                        { new Sand(1),2000 }
                    }
                },
                { CollectionMethod.Take, new Dictionary<Item,int>()
                    {
                        { new Rock(1),8000 }
                    }
                }
            };
        }
    }
}
