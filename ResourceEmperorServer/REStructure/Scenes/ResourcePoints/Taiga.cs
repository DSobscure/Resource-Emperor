using System.Collections.Generic;
using REProtocol;
using REStructure.Items.Materials;

namespace REStructure.Scenes.ResourcePoints
{
    public class Taiga : ResourcePoint
    {
        protected Taiga() { }
        public Taiga(List<Pathway> allPathways)
        {
            name = "針葉林";
            this.allPathways = allPathways;
            collectionList = new Dictionary<CollectionMethod, Dictionary<Item, int>>()
            {
                { CollectionMethod.Hew, new Dictionary<Item, int>()
                    {
                        { new Log(1),5000 },
                        { new Cypress(1),4000 }
                    }
                }
            };
        }
    }
}
