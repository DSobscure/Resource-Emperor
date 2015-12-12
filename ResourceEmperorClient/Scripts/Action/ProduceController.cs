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
        PhotonGlobal.PS.OnProduceResponse += ProduceEventAction;
    }
    void OnDestroy()
    {
        PhotonGlobal.PS.OnProduceResponse -= ProduceEventAction;
    }

    public void StartProduce()
    {
        if (applianceContentController.selectedAppliance != null && applianceContentController.selectedProduceMethod != null && GameGlobal.Inventory != null)
        {
            if (applianceContentController.selectedProduceMethod.Sufficient(GameGlobal.Inventory))
            {
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
    public void ProduceEventAction(bool produceStatus, ApplianceID applianceID, ProduceMethodID produceMethodID, ApplianceID selectedApplianceID, bool isNewAppliance)
    {
        applianceContentController.workingAnimation.SetActive(false);
        applianceContentController.remainTime = 0;
        if (produceStatus)
        {
            if (GameGlobal.Appliances.ContainsKey(applianceID) && GameGlobal.Appliances[applianceID].methods.ContainsKey(produceMethodID))
            {
                if (isNewAppliance)
                {
                    applianceContentController.SelectAppliance(selectedApplianceID);
                    applianceContentController.UpdateApplianceScelectPanel();
                }
                else
                {
                    applianceContentController.UpdateMethodMaterial();
                }
            }
        }
        applianceContentController.processButton.enabled = applianceContentController.selectedProduceMethod.Sufficient(GameGlobal.Inventory);
        applianceContentController.processButton.image.color = (applianceContentController.processButton.enabled) ? Color.white : Color.grey;
    }
}
