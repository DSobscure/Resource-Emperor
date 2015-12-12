using UnityEngine;
using System.Collections.Generic;

public class RankingController : MonoBehaviour
{
    internal Dictionary<string, int> ranking;
    [SerializeField]
    private RankingPanelController rankingPanelController;

    void Start ()
    {
        PhotonGlobal.PS.OnGetRankingResponse += GetRankingEvent;
    }
    void OnDestroy()
    {
        PhotonGlobal.PS.OnGetRankingResponse -= GetRankingEvent;
    }

    public void GetRanking()
    {
        PhotonGlobal.PS.GetRanking();
    }

    private void GetRankingEvent(bool status, Dictionary<string, int> ranking)
    {
        if(status)
        {
            this.ranking = ranking;
            rankingPanelController.rankingFullPanel.gameObject.SetActive(true);
            rankingPanelController.Refresh();
        }
    }
}
