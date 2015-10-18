using System.Collections.Generic;
using REProtocol;
using REStructure.Items.Materials;

namespace REStructure.Scenes.ResourcePoints
{
    public class DeepCave : ResourcePoint
    {
        protected DeepCave() { }
        public DeepCave(List<Pathway> allPathways)
        {
            name = "山洞深處";
            this.allPathways = allPathways;
            collectionList = new Dictionary<CollectionMethod, Dictionary<Item, int>>()
            {
                { CollectionMethod.Dig, new Dictionary<Item, int>()
                    {
                        { new Coal(1), 2000 },
                        { new Rock(1), 3000 }
                    }
                }
            };
        }
    }
}
