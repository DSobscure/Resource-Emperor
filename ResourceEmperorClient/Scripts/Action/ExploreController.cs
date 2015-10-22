using UnityEngine;
using System.Collections.Generic;
using REStructure;
using REStructure.Scenes;

public class ExploreController : MonoBehaviour
{
    public void Explorer()
    {
        if(GameGlobal.Player.Location is Wilderness)
        {
            PhotonGlobal.PS.Explore();
        }
    }
}
