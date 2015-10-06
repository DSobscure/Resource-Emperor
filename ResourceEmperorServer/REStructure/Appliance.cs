using System.Collections.Generic;
using System;
using REProtocol;
using Newtonsoft.Json;

namespace REStructure
{
    public abstract class Appliance
    {
        [JsonProperty("id")]
        public abstract ApplianceID id { get; protected set; }
        [JsonProperty("name")]
        public abstract string name { get; protected set; }
        [JsonProperty("methods")]
        public abstract Dictionary<ProduceMethodID,ProduceMethod> methods { get; protected set; }
        protected Appliance() { }
    }
}
