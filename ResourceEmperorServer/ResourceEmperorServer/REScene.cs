using System;
using RESerializable;

namespace ResourceEmperorServer
{
    public class REScene
    {
        public int uniqueID { get; set; }
        public string name { get; set; }

        public SerializableScene Serialize()
        {
            return new SerializableScene(uniqueID, name);
        }
    }
}
