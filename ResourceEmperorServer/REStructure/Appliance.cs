using System.Collections.Generic;
using System;
using REProtocol;

namespace REStructure
{
    public abstract class Appliance
    {
        public abstract ApplianceID id { get; protected set; }
        public abstract string name { get; protected set; }
        public abstract Dictionary<ProduceMethodID,ProduceMethod> methods { get; protected set; }
        protected Appliance() { }
    }
}
