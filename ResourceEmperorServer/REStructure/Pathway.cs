using REStructure.Scenes;

namespace REStructure
{
    public class Pathway
    {
        public int uniqueID { get; protected set; }
        public Scene endPoint1 { get; protected set; }
        public Scene endPoint2 { get; protected set; }
        public int distance { get; protected set; }
        public double discoveredProbability { get; protected set; }
        private static int pathwayCount = 0;

        protected Pathway() { }
        public Pathway(Scene endPoint1, Scene endPoint2, int distance, double discoveredProbability)
        {
            uniqueID = pathwayCount++;
            this.endPoint1 = endPoint1;
            this.endPoint2 = endPoint2;
            this.distance = distance;
            this.discoveredProbability = discoveredProbability;
        }
    }
}
