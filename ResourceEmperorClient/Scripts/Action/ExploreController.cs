using UnityEngine;
using System.Collections.Generic;
using REStructure;
using REStructure.Scenes;

public class ExploreController : MonoBehaviour
{
    [SerializeField]
    private ExplorePanelController explorePanelController;

    void Start()
    {
        PhotonGlobal.PS.ExploreEvent += ExploreEventAction;
    }
    void OnDestroy()
    {
        PhotonGlobal.PS.ExploreEvent -= ExploreEventAction;
    }

    public void Explorer()
    {
        if(GameGlobal.Player.Location is Wilderness)
        {
            PhotonGlobal.PS.Explore();
        }
    }

    private void ExploreEventAction(bool status, string debugMessage, List<Pathway> paths)
    {
        if (status)
        {
            if (GameGlobal.Player.Location is Wilderness)
            {
                List<Pathway> discoveredPaths = (GameGlobal.Player.Location as Wilderness).discoveredPaths;
                foreach (Pathway path in paths)
                {
                    if (!discoveredPaths.Contains(path))
                    {
                        discoveredPaths.Add(path);
                    }
                }
                explorePanelController.ShowPathways();
            }
        }
        else
            Debug.Log(debugMessage);
    }
}
