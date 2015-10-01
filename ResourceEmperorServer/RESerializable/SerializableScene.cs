using System;

namespace RESerializable
{
    [Serializable]
    public class SerializableScene
    {
        public int uniqueID;
        public string name;

        public SerializableScene(int uniqueID, string name)
        {
            this.uniqueID = uniqueID;
            this.name = name;
        }
    }
}
