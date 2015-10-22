using UnityEngine;
using REStructure;
using REProtocol;
using REStructure.Scenes;

public class WorkRoomSceneEventController : MonoBehaviour, ISceneEventController
{
    //controllers
    [SerializeField]
    private InventoryController inventoryController;
    [SerializeField]
    private ApplianceContentController applianceContentController;

    public void InitialEvents()
    {
        PhotonGlobal.PS.ProduceEvent += ProduceEventAction;
        PhotonGlobal.PS.DiscardItemEvent += DiscardItemEventAction;
        PhotonGlobal.PS.GoToSceneEvent += GoToSceneEventAction;
    }
    public void ClearEvents()
    {
        PhotonGlobal.PS.ProduceEvent -= ProduceEventAction;
        PhotonGlobal.PS.DiscardItemEvent -= DiscardItemEventAction;
        PhotonGlobal.PS.GoToSceneEvent -= GoToSceneEventAction;
    }

    void Start ()
    {
        InitialEvents();
    }

    public void ProduceEventAction(bool produceStatus, string debugMessage, ApplianceID applianceID, ProduceMethodID produceMethodID)
    {
        applianceContentController.workingAnimation.SetActive(false);
        GameGlobal.Player.IsWorking = false;
        applianceContentController.remainTime = 0;
        if (produceStatus)
        {
            if (GameGlobal.Appliances.ContainsKey(applianceID) && GameGlobal.Appliances[applianceID].methods.ContainsKey(produceMethodID))
            {
                object[] results;
                applianceContentController.selectedAppliance = GameGlobal.Appliances[applianceID];
                applianceContentController.selectedProduceMethod = applianceContentController.selectedAppliance.methods[produceMethodID];
                if (applianceContentController.selectedProduceMethod.Process(GameGlobal.Inventory, out results))
                {
                    foreach (object result in results)
                    {
                        if (result is Item)
                        {
                            Item item = result as Item;
                            if (GameGlobal.Inventory.ContainsKey(item.id))
                            {
                                GameGlobal.Inventory[item.id].Increase(item.itemCount);
                            }
                            else
                            {
                                GameGlobal.Inventory.Add(item.id, item.Clone() as Item);
                            }
                        }
                        else if (result is Appliance)
                        {
                            Appliance appliance = result as Appliance;
                            if (!GameGlobal.Appliances.ContainsKey(appliance.id))
                            {
                                if (applianceContentController.selectedAppliance is IUpgradable)
                                {
                                    IUpgradable target = applianceContentController.selectedAppliance as IUpgradable;
                                    if (target.UpgradeCheck(appliance))
                                    {
                                        GameGlobal.Appliances.Remove(applianceContentController.selectedAppliance.id);
                                        Appliance upgraded = target.Upgrade() as Appliance;
                                        GameGlobal.Appliances.Add(upgraded.id, upgraded);
                                        applianceContentController.SelectAppliance(upgraded.id);
                                    }
                                    else
                                    {
                                        GameGlobal.Appliances.Add(appliance.id, appliance);
                                    }
                                }
                                else
                                {
                                    GameGlobal.Appliances.Add(appliance.id, appliance);
                                }
                                applianceContentController.UpdateApplianceScelectPanel();
                            }
                        }
                    }
                    applianceContentController.UpdateMethodMaterial();
                    applianceContentController.processButton.enabled = applianceContentController.selectedProduceMethod.Sufficient(GameGlobal.Inventory);
                    applianceContentController.processButton.image.color = (applianceContentController.processButton.enabled) ? Color.white : Color.grey;
                    inventoryController.ShowInventory();
                }
            }
        }
        else
        {
            applianceContentController.processButton.enabled = applianceContentController.selectedProduceMethod.Sufficient(GameGlobal.Inventory);
            applianceContentController.processButton.image.color = (applianceContentController.processButton.enabled) ? Color.white : Color.grey;
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
    private void WalkPathEventAction(bool status, string debugMessage, Scene targetScene)
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
        }
    }
}
