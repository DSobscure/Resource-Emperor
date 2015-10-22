using System.Collections.Generic;
using REStructure.Scenes;
using REStructure.Scenes.ResourcePoints;

namespace REStructure
{
    public class GlobalMap
    {
        public Dictionary<int,Scene> scenes { get; protected set; }
        public Dictionary<int,Pathway> paths { get; protected set; }
        public List<Town> towns { get; protected set; }

        public GlobalMap()
        {
            scenes = new Dictionary<int,Scene>();
            paths = new Dictionary<int,Pathway>();
            towns = new List<Town>();

            #region 起始村莊地圖
            Town startTown = new Town("起始村莊");
            {
                #region 邊緣地帶地圖
                Wilderness fringe = new Wilderness("邊緣地帶", new List<Pathway>());
                {
                    #region 往海邊的道路地圖
                    Wilderness toBeachRoad = new Wilderness("往海邊的道路", new List<Pathway>());
                    {
                        #region 矮樹叢地圖
                        Wilderness shortBush = new Wilderness("矮樹叢", new List<Pathway>());
                        {
                            #region 茂密的森林地圖
                            LushForest lushForest = new LushForest("茂密的森林", new List<Pathway>());
                            #endregion
                            #region 叢森地圖
                            Jungle jungle = new Jungle("叢森", new List<Pathway>());
                            {
                                #region 雨林地圖
                                RainForest rainForest = new RainForest("雨林", new List<Pathway>());
                                #endregion
                                #region 溪流地圖
                                Rivulet rivulet = new Rivulet("溪流", new List<Pathway>());
                                #endregion
                                AddScene(jungle, new List<PathwayInfo>()
                                {
                                    new PathwayInfo() { Scene = rainForest, Distance = 100, DiscoveredProbability = 0.3 },
                                    new PathwayInfo() { Scene = rivulet, Distance = 50, DiscoveredProbability = 0.6 }
                                });
                            }
                            #endregion
                            AddScene(shortBush, new List<PathwayInfo>()
                            {
                                new PathwayInfo() { Scene = lushForest, Distance = 20, DiscoveredProbability = 0.5 },
                                new PathwayInfo() { Scene = jungle, Distance = 60, DiscoveredProbability = 0.4 }
                            });
                        }
                        #endregion
                        #region 灌木叢地圖
                        Bush bush = new Bush("灌木叢", new List<Pathway>());
                        {
                            DeeperBush deeperBush = new DeeperBush("更深的灌木叢", new List<Pathway>());
                            AddScene(bush, new List<PathwayInfo>()
                            {
                                new PathwayInfo() { Scene = deeperBush, Distance = 100, DiscoveredProbability = 0.1 }
                            });
                        }
                        #endregion
                        #region 防風林地圖
                        Wilderness windbreaks = new Wilderness("防風林", new List<Pathway>());
                        {
                            CostalGrove costalGrove = new CostalGrove("海岸樹叢", new List<Pathway>());
                            Beach beach = new Beach("海灘", new List<Pathway>());
                            AddScene(windbreaks, new List<PathwayInfo>()
                            {
                                new PathwayInfo() { Scene = costalGrove, Distance = 8, DiscoveredProbability = 0.9 },
                                new PathwayInfo() { Scene = beach, Distance = 12, DiscoveredProbability = 1 }
                            });
                        }
                        #endregion
                        AddScene(toBeachRoad, new List<PathwayInfo>()
                        {
                            new PathwayInfo() { Scene = shortBush, Distance = 20, DiscoveredProbability = 0.6 },
                            new PathwayInfo() { Scene = bush, Distance = 40, DiscoveredProbability = 0.4 },
                            new PathwayInfo() { Scene = windbreaks, Distance = 10, DiscoveredProbability = 0.8 }
                        });
                    }
                    #endregion
                    #region 往森林的道路地圖
                    Wilderness toForestRoad = new Wilderness("往森林的道路", new List<Pathway>());
                    {
                        #region 草原地圖
                        Grassland grassland = new Grassland("草原", new List<Pathway>());
                        {
                            #region 荒野地圖
                            Wilderness wilderness = new Wilderness("荒野", new List<Pathway>());
                            {
                                #region 山洞地圖
                                Cave cave = new Cave("山洞", new List<Pathway>());
                                {
                                    #region 礦坑地圖
                                    Mine mine = new Mine("礦坑", new List<Pathway>());
                                    {
                                        #region 更深入的礦坑地圖
                                        DeeperMine deeperMine = new DeeperMine("更深入的礦坑", new List<Pathway>());
                                        #endregion
                                        AddScene(mine, new List<PathwayInfo>()
                                        {
                                            new PathwayInfo() { Scene = deeperMine, Distance = 19, DiscoveredProbability = 0.05 }
                                        });
                                    }
                                    #endregion
                                    AddScene(cave, new List<PathwayInfo>()
                                    {
                                        new PathwayInfo() { Scene = mine, Distance = 7, DiscoveredProbability = 0.15 }
                                    });
                                }
                                #endregion
                                AddScene(wilderness, new List<PathwayInfo>()
                                {
                                    new PathwayInfo() { Scene = cave, Distance = 50, DiscoveredProbability = 0.4 }
                                });
                            }
                            #endregion
                            AddScene(grassland, new List<PathwayInfo>()
                            {
                                new PathwayInfo() { Scene = wilderness, Distance = 30, DiscoveredProbability = 0.95 }
                            });
                        }
                        #endregion
                        #region 森林小徑地圖
                        Wilderness forestPath = new Wilderness("森林小徑", new List<Pathway>());
                        {
                            #region 森林地圖
                            Forest forest = new Forest("森林", new List<Pathway>());
                            {
                                #region 更深的森林地圖
                                DeeperForest deeperForest = new DeeperForest("更深的森林", new List<Pathway>());
                                {
                                    #region 湖泊地圖
                                    Lake lake = new Lake("湖泊", new List<Pathway>());
                                    #endregion
                                    AddScene(deeperForest, new List<PathwayInfo>()
                                    {
                                        new PathwayInfo() { Scene = lake, Distance = 100, DiscoveredProbability = 0.3 }
                                    });
                                }
                                #endregion
                                AddScene(forest, new List<PathwayInfo>()
                                {
                                    new PathwayInfo() { Scene = deeperForest, Distance = 40, DiscoveredProbability = 0.6 }
                                });
                            }
                            #endregion
                            #region 山路地圖
                            Wilderness mountainerRoad = new Wilderness("山路", new List<Pathway>());
                            {
                                #region 針葉林地圖
                                Taiga taiga = new Taiga("針葉林", new List<Pathway>());
                                {
                                    #region 高山草原地圖
                                    Wilderness alpineGrassland = new Wilderness("高山草原", new List<Pathway>());
                                    #endregion
                                    AddScene(taiga, new List<PathwayInfo>()
                                    {
                                        new PathwayInfo() { Scene = alpineGrassland, Distance = 200, DiscoveredProbability = 0.4 }
                                    });
                                }
                                #endregion
                                AddScene(mountainerRoad, new List<PathwayInfo>()
                                {
                                    new PathwayInfo() { Scene = taiga, Distance = 80, DiscoveredProbability = 0.6 }
                                });
                            }
                            #endregion
                            #region 溪谷地圖
                            Ravine ravine = new Ravine("溪谷", new List<Pathway>());
                            #endregion
                            #region 山洞地圖
                            Cave cave = new Cave("山洞", new List<Pathway>());
                            {
                                #region 山洞深處地圖
                                DeepCave deepCave = new DeepCave("山洞深處", new List<Pathway>());
                                {
                                    #region 礦坑地圖
                                    Mine mine = new Mine("礦坑", new List<Pathway>());
                                    {
                                        #region 更深入的礦坑地圖
                                        DeeperMine deeperMine = new DeeperMine("更深入的礦坑", new List<Pathway>());
                                        #endregion
                                        AddScene(mine, new List<PathwayInfo>()
                                        {
                                            new PathwayInfo() { Scene = deeperMine, Distance = 50, DiscoveredProbability = 0.4 }
                                        });
                                    }
                                    #endregion
                                    #region 礦脈地圖
                                    Lode lode = new Lode("礦脈", new List<Pathway>());
                                    #endregion
                                    AddScene(deepCave, new List<PathwayInfo>()
                                    {
                                        new PathwayInfo() { Scene = mine, Distance = 80, DiscoveredProbability = 0.5 },
                                        new PathwayInfo() { Scene = lode, Distance = 180, DiscoveredProbability = 0.1 }
                                    });
                                }
                                #endregion
                                AddScene(cave, new List<PathwayInfo>()
                                {
                                    new PathwayInfo() { Scene = deepCave, Distance = 30, DiscoveredProbability = 0.8 }
                                });
                            }
                            #endregion
                            AddScene(forestPath, new List<PathwayInfo>()
                            {
                                new PathwayInfo() { Scene = forest, Distance = 20, DiscoveredProbability = 0.95 },
                                new PathwayInfo() { Scene = mountainerRoad, Distance = 40, DiscoveredProbability = 0.6 },
                                new PathwayInfo() { Scene = ravine, Distance = 80, DiscoveredProbability = 0.15 },
                                new PathwayInfo() { Scene = cave, Distance = 90, DiscoveredProbability = 0.3 }
                            });
                        }
                        #endregion
                        AddScene(toForestRoad, new List<PathwayInfo>()
                        {
                            new PathwayInfo() { Scene = grassland, Distance = 40, DiscoveredProbability = 0.9 },
                            new PathwayInfo() { Scene = forestPath, Distance = 5, DiscoveredProbability = 1 }
                        });
                    }
                    #endregion
                    AddScene(fringe, new List<PathwayInfo>()
                    {
                        new PathwayInfo() { Scene = toBeachRoad, Distance = 10, DiscoveredProbability = 0.9 },
                        new PathwayInfo() { Scene = toForestRoad, Distance = 14, DiscoveredProbability = 0.9 }
                    });
                }
                #endregion
                AddScene(startTown, new List<PathwayInfo>()
                {
                    new PathwayInfo() { Scene = fringe, Distance = 10, DiscoveredProbability = 1 }
                });
            }
            #endregion
            towns.Add(startTown);
        }

        public void AddScene(Scene sourceScene, List<PathwayInfo> pathInfos)
        {
            //if(!scenes.ContainsKey(sourceScene.uniqueID))
                scenes.Add(sourceScene.uniqueID, sourceScene);
            foreach (var info in pathInfos)
            {
                if (!scenes.ContainsKey(info.Scene.uniqueID))
                    scenes.Add(info.Scene.uniqueID, info.Scene);
                Pathway path = new Pathway(sourceScene, info.Scene, info.Distance, info.DiscoveredProbability);
                sourceScene.AddPath(path);
                info.Scene.AddPath(path);
                paths.Add(path.uniqueID, path);
            }
        }
    }
}
