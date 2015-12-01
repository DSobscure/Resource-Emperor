using UnityEngine;
using System.Collections.Generic;
using REStructure;
using REStructure.Scenes;

public class SceneController : MonoBehaviour
{
    void Start()
    {
        PhotonGlobal.PS.GoToSceneEvent += GoToSceneEventAction;
        PhotonGlobal.PS.WalkPathEvent += WalkPathEventAction;
    }

    void OnDestroy()
    {
        PhotonGlobal.PS.GoToSceneEvent -= GoToSceneEventAction;
        PhotonGlobal.PS.WalkPathEvent -= WalkPathEventAction;
    }

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

    private void GoToSceneEventAction(bool status, string debugMessage, Scene targetScene)
    {
        if (status)
        {
            GameGlobal.Player.Location = targetScene;
            if (targetScene is Town)
            {
                Application.LoadLevel("TownScene");
            }
            else if (targetScene is ResourcePoint)
            {
                Application.LoadLevel("ResourcePointScene");
            }
            else if (targetScene is Wilderness)
            {
                Application.LoadLevel("WildernessScene");
            }
            else if (targetScene is Room)
            {
                Application.LoadLevel("WorkRoomScene");
            }
        }
    }
    private void WalkPathEventAction(bool status, string debugMessage, Pathway path, Scene targetScene, List<string> messages)
    {
        if (status)
        {
            GameGlobal.Player.Location = targetScene;
            if (targetScene is Wilderness)
            {
                Wilderness targetWilderness = targetScene as Wilderness;
                if (!targetWilderness.discoveredPaths.Contains(path))
                    targetWilderness.discoveredPaths.Add(path);
                targetWilderness.messages.Clear();
                foreach(string message in messages)
                    targetWilderness.messages.Enqueue(message);
            }
            if (targetScene is Town)
            {
                Application.LoadLevel("TownScene");
            }
            else if (targetScene is ResourcePoint)
            {
                Application.LoadLevel("ResourcePointScene");
            }
            else if (targetScene is Wilderness)
            {
                Application.LoadLevel("WildernessScene");
            }
        }
    }
}
