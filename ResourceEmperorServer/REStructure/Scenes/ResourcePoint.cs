using System.Collections.Generic;
using REProtocol;
using Newtonsoft.Json;
using System;
using REStructure.Items;
using REStructure.Items.Tools;

namespace REStructure.Scenes
{
    public class ResourcePoint : Wilderness
    {
        [JsonProperty("collectionList")]
        public Dictionary<CollectionMethod,Dictionary<Item,int>> collectionList { get; protected set; }

        protected ResourcePoint() { }
        public ResourcePoint(int uniqueID, string name, List<Pathway> allPathways, Dictionary<CollectionMethod, Dictionary<Item, int>> collectionList)
            : base(name, allPathways)
        {
            this.collectionList = collectionList;
        }

        public bool ToolCheck(CollectionMethod method, Tool tool)
        {
            switch (method)
            {
                case CollectionMethod.Hew:
                    {
                        return tool is Axe;
                    }
                case CollectionMethod.Dig:
                    {
                        return tool is Pickaxe;
                    }
            }
            return true;
        }
        public Item Collect(CollectionMethod method, Tool tool)
        {
            if(ToolCheck(method,tool))
            {
                return GetMaterial(method);
            }
            else
            {
                return null;
            }
        }
        private Item GetMaterial(CollectionMethod method)
        {
            Dictionary<int,Item> materialTable = new Dictionary<int, Item>();
            int offset = 0;
            foreach(var pair in collectionList[method])
            {
                offset += pair.Value;
                materialTable.Add(offset, pair.Key);
            }
            Random random = new Random();
            int result = random.Next(1, 10000);
            foreach(var pair in materialTable)
            {
                if(result <= pair.Key)
                {
                    return pair.Value.Clone() as Item;
                }
            }
            return null;
        }
    }
}
