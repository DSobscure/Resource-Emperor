using System;
using REProtocol;

namespace REStructure.Items.Materials
{
    public class Log : PlantMaterial
    {
        public Log() : base() { }
        public Log(int itemCount) : base(itemCount)
        {
            id = ItemID.Log;
            name = "木頭";
            description = "";
            type = PlantType.Wood;
        }
    }
}
