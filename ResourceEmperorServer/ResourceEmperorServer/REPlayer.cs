using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REStructure;
using Newtonsoft.Json;
using REProtocol;
using REStructure.Appliances;
using REStructure.Items.Materials;

namespace ResourceEmperorServer
{
    public class REPlayer : Player
    {
        public REPeer peer { get; set; }
        public Inventory inventory { get; set; }
        public Dictionary<ApplianceID,Appliance> appliances { get; set; }
        public Scene Location { get; set; }

        public REPlayer() { }
        public REPlayer(int uniqueID, string account, REPeer peer) : base(uniqueID, account, 100)
        {
            this.peer = peer;
            inventory = new Inventory()
            {
                new Log(150),
                new IronOre(20),
                new Rock(30),
                new Hemp(40),
                new Oak(20),
                new Cypress(20),
                new Clay(50),
                new CopperOre(20),
                new Coal(100),
                new Water(100),
                new Cotton(100)
            };
            appliances = new Dictionary<ApplianceID, Appliance>() { { ApplianceID.Machete, new Machete() } };
        }
        public REPlayer(int uniqueID, string account, string inventoryDataString, string appliancesDataString, int money, REPeer peer) : base(uniqueID, account, money)
        {
            this.peer = peer;
            inventory = JsonConvert.DeserializeObject<Inventory>(inventoryDataString, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
            appliances = JsonConvert.DeserializeObject<Dictionary<ApplianceID, Appliance>>(appliancesDataString, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
        }

        public Player Serialize()
        {
            return new Player(uniqueID, account, money);
        }
    }
}
