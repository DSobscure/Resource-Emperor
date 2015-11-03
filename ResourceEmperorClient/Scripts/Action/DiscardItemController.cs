using UnityEngine;
using System.Linq;
using REProtocol;

public class DiscardItemController : MonoBehaviour
{
    [SerializeField]
    private InventoryPanelController inventoryPanelController;

    void Start()
    {
        PhotonGlobal.PS.DiscardItemEvent += DiscardItemEventAction;
    }

    void OnDestroy()
    {
        PhotonGlobal.PS.DiscardItemEvent -= DiscardItemEventAction;
    }

    public void DiscardItem()
    {
        if (inventoryPanelController.blockPositions.ContainsValue(inventoryPanelController.selectedItemIndex))
        {
            var pair = inventoryPanelController.blockPositions.First(x => x.Value == inventoryPanelController.selectedItemIndex);
            PhotonGlobal.PS.DiscardItem(pair.Key, 1);
            inventoryPanelController.discardButton.enabled = false;
            inventoryPanelController.discardButton.image.color = Color.grey;
        }
    }

    private void DiscardItemEventAction(bool discardStatus, string debugMessage, ItemID itemID, int itemCount)
    {
        if (discardStatus && GameGlobal.Inventory.ContainsKey(itemID))
        {
            if (itemCount > 0)
            {
                GameGlobal.Inventory[itemID].Reset();
                GameGlobal.Inventory[itemID].Increase(itemCount);
            }
            else
            {
                GameGlobal.Inventory.Remove(itemID);
                inventoryPanelController.blockPositions.Remove(itemID);
            }
            inventoryPanelController.ShowInventory();
        }
        inventoryPanelController.discardButton.enabled = true;
        inventoryPanelController.discardButton.image.color = inventoryPanelController.discardButtonOriginColor;
    }
}
