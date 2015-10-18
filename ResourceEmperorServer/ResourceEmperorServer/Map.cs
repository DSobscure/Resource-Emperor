using System;
using System.Collections.Generic;
using REStructure;

namespace ResourceEmperorServer
{
    public class Map
    {
        public HashSet<Scene> scenes { get; protected set; }
        public HashSet<Pathway> paths { get; set; }

        public void AddScene(Scene sourceScene,List<PathwayInfo> targetInfos)
        {
            scenes.Add(sourceScene);
            foreach (var info in targetInfos)
            {
                scenes.Add(info.Scene);
                Pathway path = new Pathway(sourceScene, info.Scene, info.Distance, info.DiscoveredProbability);
                sourceScene.AddPath(path);
                info.Scene.AddPath(path);
                paths.Add(path);
            }
        }
    }
}
