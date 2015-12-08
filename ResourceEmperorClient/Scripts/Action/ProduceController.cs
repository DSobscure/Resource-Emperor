using REProtocol;
using REStructure;
using UnityEngine;

public class ProduceController : MonoBehaviour
{
    [SerializeField]
    private ApplianceContentController applianceContentController;
    [SerializeField]
    private InventoryPanelController inventoryPanelController;

    void Start ()
    {
        PhotonGlobal.PS.ProduceEvent += ProduceEventAction;
    }
    void OnDestroy()
    {
        PhotonGlobal.PS.ProduceEvent -= ProduceEventAction;
    }

    public void StartProduce()
    {
        if (applianceContentController.selectedAppliance != null && applianceContentController.selectedProduceMethod != null && GameGlobal.Inventory != null)
        {
            if (applianceContentController.selectedProduceMethod.Sufficient(GameGlobal.Inventory))
            {
                GameGlobal.Player.IsWorking = true;
                applianceContentController.workingAnimation.SetActive(true);
                applianceContentController.remainTime = applianceContentController.selectedProduceMethod.processTime;
                applianceContentController.processSlider.maxValue = applianceContentController.selectedProduceMethod.processTime;
                applianceContentController.processSlider.value = applianceContentController.selectedProduceMethod.processTime;
                PhotonGlobal.PS.Produce(applianceContentController.selectedAppliance, applianceContentController.selectedProduceMethod);
                applianceContentController.processButton.enabled = false;
                applianceContentController.processButton.image.color = Color.grey;
                applianceContentController.cancelButton.enabled = true;
                applianceContentController.cancelButton.image.color = Color.white;
            }
        }
    }
    public void CancelProduce()
    {
        applianceContentController.cancelButton.enabled = false;
        applianceContentController.cancelButton.image.color = Color.grey;
        PhotonGlobal.PS.CancelProduce();
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
                            GameGlobal.Inventory.Stack(item);
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
                    inventoryPanelController.ShowInventory();
                }
            }
        }
        else
        {
            applianceContentController.processButton.enabled = applianceContentController.selectedProduceMethod.Sufficient(GameGlobal.Inventory);
            applianceContentController.processButton.image.color = (applianceContentController.processButton.enabled) ? Color.white : Color.grey;
        }
    }
}
