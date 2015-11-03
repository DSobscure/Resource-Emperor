using UnityEngine;
using System.Collections.Generic;

public class RankingController : MonoBehaviour
{
    internal Dictionary<string, int> ranking;
    [SerializeField]
    private RankingPanelController rankingPanelController;

    void Start ()
    {
        PhotonGlobal.PS.GetRankingEvent += GetRankingEvent;
    }
    void OnDestroy()
    {
        PhotonGlobal.PS.GetRankingEvent -= GetRankingEvent;
    }

    public void GetRanking()
    {
        PhotonGlobal.PS.GetRanking();
    }

    private void GetRankingEvent(bool status, string debugMessage, Dictionary<string, int> ranking)
    {
        if(status)
        {
            this.ranking = ranking;
            rankingPanelController.rankingFullPanel.gameObject.SetActive(true);
            rankingPanelController.Refresh();
        }
        else
        {
            Debug.Log(debugMessage);
        }
    }
}
