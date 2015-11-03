using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using REStructure.Scenes;
using REProtocol;
using REStructure.Items;

public class CollectionButtonController : MonoBehaviour
{
    [SerializeField]
    private CollectMaterialController collectMaterial;
    [SerializeField]
    private InventoryPanelController inventoryPanelController;

    [SerializeField]
    private RectTransform controlButtonPrefab;
    [SerializeField]
    private RectTransform controlPanel;

    void Start()
    {
        ShowCollectButtons();
    }

    public void ShowCollectButtons()
    {
        if(GameGlobal.Player.Location is ResourcePoint)
        {
            ResourcePoint resourcePoint = GameGlobal.Player.Location as ResourcePoint;
            var collectionMethods = resourcePoint.collectionList.Keys;
            int index = 0;
            float xoffset = -controlPanel.rect.width / 2 + 100;
            foreach (var method in collectionMethods)
            {
                RectTransform collectButton = Instantiate(controlButtonPrefab);
                collectButton.transform.SetParent(controlPanel);
                collectButton.localScale = Vector3.one;
                collectButton.localPosition = new Vector3(xoffset+index* collectButton.rect.width, 0);
                string methodName = "";
                switch (method)
                {
                    case CollectionMethod.Take:
                        methodName = "採集";
                        break;
                    case CollectionMethod.Hew:
                        methodName = "伐木";
                        break;
                    case CollectionMethod.Dig:
                        methodName = "挖掘";
                        break;
                    case CollectionMethod.Fill:
                        methodName = "裝取";
                        break;
                }
                collectButton.GetComponent<Button>().enabled = resourcePoint.ToolCheck(method, GameGlobal.Player.SelectedItem as Tool);
                collectButton.GetComponent<Button>().image.color = (collectButton.GetComponent<Button>().enabled) ? Color.white : Color.grey;
                collectButton.GetChild(0).GetComponent<Text>().text = methodName;
                CollectionMethod collectionMethod = method;
                collectButton.GetComponent<Button>().onClick.AddListener(() => collectMaterial.Collect(collectionMethod));
                index++;
            }
        }
    }
}
