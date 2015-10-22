using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using REStructure;
using REStructure.Scenes;

public class ExplorePanelController : MonoBehaviour
{
    [SerializeField]
    private Text locationText;
    [SerializeField]
    private SceneController sceneController;
    [SerializeField]
    private GameObject explorePanel;
    [SerializeField]
    private RectTransform pathwayPanel;
    [SerializeField]
    private RectTransform pathwayPrefab;

    void Start()
    {
        ShowPathways();
    }

    public void ShowPathways()
    {
        explorePanel.SetActive(true);
        for (int i = pathwayPanel.childCount - 1; i >= 0; i--)
        {
            Destroy(pathwayPanel.GetChild(i).gameObject);
        }
        int index = 0;
        float yoffset = pathwayPanel.rect.height/ 2 - pathwayPrefab.rect.height/2;
        Scene location = GameGlobal.Player.Location;
        locationText.text = location.name;
        List<Pathway> paths = (location is Wilderness)?(location as Wilderness).discoveredPaths : location.paths;
        foreach (Pathway path in paths)
        {
            RectTransform pathway = Instantiate(pathwayPrefab);
            pathway.transform.SetParent(pathwayPanel);
            pathway.localScale = Vector3.one;
            pathway.localPosition = new Vector3(0f, yoffset - index * pathwayPrefab.rect.height);
            pathway.GetChild(0).GetComponent<Text>().text = "往" + ((path.endPoint1 == location) ? path.endPoint2.name : path.endPoint1.name);
            pathway.GetChild(1).GetComponent<Text>().text = "距離： " + path.distance.ToString() + "m";
            pathway.GetChild(2).GetComponent<Text>().text = "時間： " + "尚未處理";
            int pathIndex = index;
            pathway.GetChild(3).GetComponent<Button>().onClick.AddListener(() => sceneController.GoPath(pathIndex));
            index++;
        }
    }
}
