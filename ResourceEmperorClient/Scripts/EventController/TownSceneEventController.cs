using UnityEngine;
using REProtocol;
using REStructure;
using REStructure.Scenes;

public class TownSceneEventController : MonoBehaviour, ISceneEventController
{
    //controllers
    [SerializeField]
    private InventoryController inventoryController;
    [SerializeField]
    MessagePanelController messageController;

    public void InitialEvents()
    {
        PhotonGlobal.PS.DiscardItemEvent += DiscardItemEventAction;
        PhotonGlobal.PS.GoToSceneEvent += GoToSceneEventAction;
        PhotonGlobal.PS.WalkPathEvent += WalkPathEventAction;
        PhotonGlobal.PS.SendMessageEvent += SendMessageEventAction;
    }
    public void ClearEvents()
    {
        PhotonGlobal.PS.DiscardItemEvent -= DiscardItemEventAction;
        PhotonGlobal.PS.GoToSceneEvent -= GoToSceneEventAction;
        PhotonGlobal.PS.WalkPathEvent -= WalkPathEventAction;
        PhotonGlobal.PS.SendMessageEvent -= SendMessageEventAction;
    }

    void Start ()
    {
        InitialEvents();
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
                inventoryController.blockPositions.Remove(itemID);
            }
            inventoryController.ShowInventory();
        }
        inventoryController.discardButton.enabled = true;
        inventoryController.discardButton.image.color = inventoryController.discardButtonOriginColor;
    }
    private void GoToSceneEventAction(bool status, string debugMessage, Scene targetScene)
    {
        if (status)
        {
            GameGlobal.Player.Location = targetScene;
            ClearEvents();
            if (targetScene is Town)
            {
                Application.LoadLevel("TownScene");
            }
            else if (targetScene is ResourcePoint)
            {
                Application.LoadLevel("ResourcePointScene");
            }
            else if (targetScene is Wilderness)
            {
                Application.LoadLevel("WildernessScene");
            }
            else if (targetScene is Room)
            {
                Application.LoadLevel("WorkRoomScene");
            }
        }
    }
    private void WalkPathEventAction(bool status, string debugMessage, Pathway path, Scene targetScene)
    {
        if (status)
        {
            GameGlobal.Player.Location = targetScene;
            if (targetScene is Wilderness)
            {
                Wilderness targetWilderness = targetScene as Wilderness;
                if (!targetWilderness.discoveredPaths.Contains(path))
                    targetWilderness.discoveredPaths.Add(path);
            }
            ClearEvents();
            if (targetScene is Town)
            {
                Application.LoadLevel("TownScene");
            }
            else if (targetScene is ResourcePoint)
            {
                Application.LoadLevel("ResourcePointScene");
            }
            else if (targetScene is Wilderness)
            {
                Application.LoadLevel("WildernessScene");
            }
        }
    }
    private void SendMessageEventAction(string senderName, string message)
    {
        messageController.AppendMessage(senderName + ": " + message);
        messageController.UpdateMessageBox();
    }
}
