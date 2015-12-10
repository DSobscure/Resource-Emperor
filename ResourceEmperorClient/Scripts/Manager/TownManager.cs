using UnityEngine;
using System.Collections;

public class TownManager : MonoBehaviour
{
	void Start ()
    {
        PhotonGlobal.PS.GetMarket();
        PhotonGlobal.PS.GetRanking();
	}
}
