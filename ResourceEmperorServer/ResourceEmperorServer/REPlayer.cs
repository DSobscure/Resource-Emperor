using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REStructure;
using RESerializable;

namespace ResourceEmperorServer
{
    public class REPlayer
    {
        public int uniqueID { get; set; }
        public string account { get; set; }
        public REPeer peer { get; set; }
        public Inventory inventory { get; set; }

        public REPlayer() { }
        public REPlayer(int uniqueID, string account, REPeer peer)
        {
            this.uniqueID = uniqueID;
            this.account = account;
            this.peer = peer;
            inventory = new Inventory();
        }

        public SerializablePlayer Serialize()
        {
            return new SerializablePlayer(uniqueID, account);
        }
    }
}
