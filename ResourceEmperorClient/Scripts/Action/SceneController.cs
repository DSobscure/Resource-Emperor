using UnityEngine;
using System.Collections.Generic;
using REStructure;
using REStructure.Scenes;

public class SceneController : MonoBehaviour
{
    public void ToTown()
    {
        PhotonGlobal.PS.GoToScene(GameGlobal.GlobalMap.towns[0].uniqueID);
    }
    public void ToRoom()
    {
        PhotonGlobal.PS.GoToScene(-1);
    }
    public void GoPath(int index)
    {
        Scene location = GameGlobal.Player.Location;
        List<Pathway> paths = (location is Wilderness) ? (location as Wilderness).discoveredPaths : location.paths;
        if(index>=0 && index < paths.Count)
        {
            PhotonGlobal.PS.WalkPath(paths[index].uniqueID);
        }
    }
}
