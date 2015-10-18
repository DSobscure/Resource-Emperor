using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace REStructure
{
    public class Pathway
    {
        public Scene endPoint1 { get; protected set; }
        public Scene endPoint2 { get; protected set; }
        public int distance { get; protected set; }
        public int discoveredProbability { get; protected set; }

        protected Pathway() { }
        public Pathway(Scene endPoint1, Scene endPoint2, int distance, int discoveredProbability)
        {
            this.endPoint1 = endPoint1;
            this.endPoint2 = endPoint2;
            this.distance = distance;
            this.discoveredProbability = discoveredProbability;
        }
    }
}
