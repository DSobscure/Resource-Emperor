using UnityEngine;
using System.Collections.Generic;
using REProtocol;
using REStructure.Items;
using REStructure.Scenes;

public class CollectMaterial : MonoBehaviour
{
    public void Collect(CollectionMethod method)
    {
        if (GameGlobal.Player.Location is ResourcePoint && (GameGlobal.Player.Location as ResourcePoint).ToolCheck(method, GameGlobal.Player.SelectedItem as Tool))
        {
            PhotonGlobal.PS.CollectMaterial(method, GameGlobal.Player.SelectedItem as Tool);
        }
    }
}
