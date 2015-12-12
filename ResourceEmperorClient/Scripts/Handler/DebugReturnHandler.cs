using UnityEngine;
using System.Collections;

public class DebugReturnHandler : MonoBehaviour
{
    void Start()
    {
        PhotonGlobal.PS.OnDebugReturn += DebugReturn;
    }
    void OnDestroy()
    {
        PhotonGlobal.PS.OnDebugReturn -= DebugReturn;
    }

    private void DebugReturn(string message)
    {
        Debug.Log(message);
    }
}
