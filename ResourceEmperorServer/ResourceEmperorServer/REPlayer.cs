using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REStructure;
using RESerializable;
using Newtonsoft.Json;
using REProtocol;
using REStructure.Appliances;
using REStructure.Items.Materials;

namespace ResourceEmperorServer
{
    public class REPlayer
    {
        public int uniqueID { get; set; }
        public string account { get; set; }
        public REPeer peer { get; set; }
        public Inventory inventory { get; set; }
        public Dictionary<ApplianceID,Appliance> appliances { get; set; }

        public REPlayer() { }
        public REPlayer(int uniqueID, string account, REPeer peer)
        {
            this.uniqueID = uniqueID;
            this.account = account;
            this.peer = peer;
            inventory = new Inventory()
            {
                { ItemID.Log, new Log(1000) },
                { ItemID.IronOre, new IronOre(1000) },
                { ItemID.Rock, new Rock(1000) },
                { ItemID.Hemp, new Hemp(1000) },
                { ItemID.Oak, new Oak(1000) },
                { ItemID.Cypress, new Cypress(1000) },
                { ItemID.Clay, new Clay(1000) },
                { ItemID.CopperOre, new CopperOre(1000) },
                { ItemID.Coal, new Coal(1000) },
                { ItemID.Water, new Water(1000) },
                { ItemID.Cotton, new Cotton(1000) }
            };
            appliances = new Dictionary<ApplianceID, Appliance>() { { ApplianceID.Machete, new Machete() } };
        }
        public REPlayer(int uniqueID, string account, string inventoryDataString, string appliancesDataString, REPeer peer)
        {
            this.uniqueID = uniqueID;
            this.account = account;
            this.peer = peer;
            inventory = JsonConvert.DeserializeObject<Inventory>(inventoryDataString, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
            appliances = JsonConvert.DeserializeObject<Dictionary<ApplianceID, Appliance>>(appliancesDataString, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
        }

        public SerializablePlayer Serialize()
        {
            return new SerializablePlayer(uniqueID, account);
        }
    }
}
