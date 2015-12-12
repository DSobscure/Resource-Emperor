using UnityEngine;
using System.Linq;
using REProtocol;
using REStructure;

public class DiscardItemController : MonoBehaviour
{
    [SerializeField]
    private InventoryPanelController inventoryPanelController;

    void Start()
    {
        PhotonGlobal.PS.OnDiscardItemResponse += DiscardItemEventAction;
    }

    void OnDestroy()
    {
        PhotonGlobal.PS.OnDiscardItemResponse -= DiscardItemEventAction;
    }

    public void DiscardItem()
    {
        if (inventoryPanelController.blockPositions.ContainsKey(inventoryPanelController.selectedItemIndex))
        {
            ItemID discardID = inventoryPanelController.blockPositions[inventoryPanelController.selectedItemIndex].id;
            PhotonGlobal.PS.DiscardItem(discardID, 1);
            inventoryPanelController.discardButton.enabled = false;
            inventoryPanelController.discardButton.image.color = Color.grey;
        }
    }

    private void DiscardItemEventAction(bool discardStatus, ItemID itemID, int itemCount)
    {
        if (discardStatus && GameGlobal.Inventory.Any(x=>x.id == itemID))
        {
            int discardCount = GameGlobal.Inventory.Where(x => x.id == itemID).Sum(x => x.itemCount) - itemCount;
            GameGlobal.Inventory.Consume(GameGlobal.Inventory.First(x => x.id == itemID).Instantiate(discardCount) as Item);
            inventoryPanelController.ShowInventory();
        }
        inventoryPanelController.discardButton.enabled = true;
        inventoryPanelController.discardButton.image.color = inventoryPanelController.discardButtonOriginColor;
    }
}
