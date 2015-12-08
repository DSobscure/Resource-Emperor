using System.Collections.Generic;

namespace REStructure.Scenes
{
    public class Town : Scene
    {
        public Market market { get; protected set; }
        protected Town() { }
        public Town(string name, Market market) : base(name)
        {
            this.market = market;
        }
    }
}
