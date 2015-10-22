﻿using System.Collections.Generic;
using REProtocol;
using Newtonsoft.Json;
using System;

namespace REStructure.Scenes
{
    public class Wilderness : Scene
    {
        [JsonProperty("allPathways")]
        public List<Pathway> discoveredPaths { get; protected set; }

        protected Wilderness() { }
        public Wilderness(string name, List<Pathway> discoveredPaths) : base(name)
        {
            this.discoveredPaths = discoveredPaths;
        }

        public List<Pathway> Explore()
        {
            List<Pathway> foundPaths = new List<Pathway>();
            Random random = new Random();
            foreach(Pathway path in paths)
            {
                if(!discoveredPaths.Contains(path)&&random.NextDouble()<=path.discoveredProbability)
                {
                    foundPaths.Add(path);
                }
            }
            return foundPaths;
        }
    }
}
