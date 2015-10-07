using System.Collections.Generic;
using System;
using REProtocol;
using Newtonsoft.Json;

namespace REStructure
{
    public abstract class Appliance
    {
        [JsonIgnore]
        public abstract ApplianceID id { get; protected set; }
        [JsonIgnore]
        public abstract string name { get; protected set; }
        [JsonIgnore]
        public abstract Dictionary<ProduceMethodID,ProduceMethod> methods { get; protected set; }
        protected Appliance() { }
    }
}
