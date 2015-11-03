using REProtocol;
using REStructure.Items;
using REStructure.Scenes;
using UnityEngine;

public class CollectMaterialController : MonoBehaviour
{
    [SerializeField]
    private InventoryPanelController inventoryPanelController;
    [SerializeField]
    private CollectionButtonController collectionButtonController;

    void Start()
    {
        PhotonGlobal.PS.CollectMaterialEvent += CollectMaterialEventAction;
        GameGlobal.Player.SelectItemEvent += collectionButtonController.ShowCollectButtons;
    }
    void OnDestroy()
    {
        PhotonGlobal.PS.CollectMaterialEvent -= CollectMaterialEventAction;
        GameGlobal.Player.SelectItemEvent -= collectionButtonController.ShowCollectButtons;
    }

    public void Collect(CollectionMethod method)
    {
        if (GameGlobal.Player.Location is ResourcePoint && (GameGlobal.Player.Location as ResourcePoint).ToolCheck(method, GameGlobal.Player.SelectedItem as Tool))
        {
            PhotonGlobal.PS.CollectMaterial(method, GameGlobal.Player.SelectedItem as Tool);
        }
    }

    private void CollectMaterialEventAction(bool status, string debugMessage)
    {
        if (status)
        {
            inventoryPanelController.ShowInventory();
        }
    }
}
