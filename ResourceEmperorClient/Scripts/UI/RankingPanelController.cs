using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class RankingPanelController : MonoBehaviour
{
    private int pageIndex = 1;
    private int pageSize = 13;
    [SerializeField]
    private RankingController rankingController;
    [SerializeField]
    private RectTransform playerRankPrefab;
    [SerializeField]
    internal RectTransform rankingPanel;
    [SerializeField]
    internal RectTransform rankingFullPanel;

    public void ShowRanking()
    {
        for (int i = rankingPanel.childCount - 1; i >= 0; i--)
        {
            Destroy(rankingPanel.GetChild(i).gameObject);
        }
        var enumerator = rankingController.ranking.GetEnumerator();
        enumerator.MoveNext();
        for (int i = (pageIndex-1)*pageSize, j = 0; j < pageSize && i < rankingController.ranking.Count; i++, j++)
        {
            RectTransform playerRank = Instantiate(playerRankPrefab);
            playerRank.transform.SetParent(rankingPanel);
            playerRank.localScale = Vector3.one;
            playerRank.localPosition = new Vector3(0, rankingPanel.rect.height/2 - playerRank.rect.height/2 - j* playerRank.rect.height);
            playerRank.GetChild(0).GetComponentInChildren<Text>().text = enumerator.Current.Key;
            playerRank.GetChild(1).GetComponentInChildren<Text>().text = enumerator.Current.Value.ToString();
            enumerator.MoveNext();
        }
    }

    public void Refresh()
    {
        pageIndex = 1;
        ShowRanking();
    }

    public void PreviousePage()
    {
        if(pageIndex > 1)
        {
            pageIndex--;
            ShowRanking();
        }
    }
    public void NextPage()
    {
        if(pageIndex < ((rankingController.ranking.Count-1)/ pageSize)+1)
        {
            pageIndex++;
            ShowRanking();
        }
    }
}
