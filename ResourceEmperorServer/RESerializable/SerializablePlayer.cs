using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using REStructure;

namespace RESerializable
{
    public class SerializablePlayer
    {
        public int uniqueID;
        public string account;

        public SerializablePlayer() { }
        public SerializablePlayer(int uniqueID, string account)
        {
            this.uniqueID = uniqueID;
            this.account = account;
        }
    }
}
