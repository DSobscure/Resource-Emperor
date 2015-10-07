using UnityEngine;
using REStructure;
using REProtocol;
using UnityEngine.UI;
using System;

public class ApplianceContentController : MonoBehaviour
{
    private Appliance selectedAppliance;
    private ProduceMethod selectedProduceMethod;

    [SerializeField]
    private Text applianceNameText;
    [SerializeField]
    private RectTransform applianceSelectPanel;
    [SerializeField]
    private InventoryController inventoryController;
    [SerializeField]
    private RectTransform applianceMenu;
    [SerializeField]
    private RectTransform methodPanel;
    [SerializeField]
    private RectTransform applianceContent;
    [SerializeField]
    private RectTransform materialPanelContent;
    [SerializeField]
    private RectTransform productPanelContent;
    [SerializeField]
    private Text produceTimeText;
    [SerializeField]
    private Button processButton;

    [SerializeField]
    private RectTransform blockPrefab;
    [SerializeField]
    private RectTransform methodPrefab;
    [SerializeField]
    private RectTransform applianceButtonPrefab;

    [SerializeField]
    private GameObject workingAnimation;
    [SerializeField]
    private Slider processSlider;
    private float remainTime;

    // Use this for initialization
    void Start ()
    {
        PhotonGlobal.PS.ProduceEvent += ProduceEventAction;
        UpdateApplianceScelectPanel();
        var enumerator = PlayerGlobal.Appliances.GetEnumerator();
        enumerator.MoveNext();
        selectedAppliance = enumerator.Current.Value;
        SelectAppliance(selectedAppliance.id);
    }

    void Update()
    {
        if(PlayerGlobal.Player.isWorking)
        {
            remainTime -= Time.deltaTime;
            processSlider.value = Convert.ToSingle(Math.Round(remainTime, 2));
        }
    }

    public void UpdateApplianceScelectPanel()
    {
        for (int i = applianceSelectPanel.childCount - 1; i >= 0; i--)
        {
            Destroy(applianceSelectPanel.GetChild(i).gameObject);
        }
        int index = 0;
        float xoffset = -300f, yoffset = 200f;
        foreach (Appliance appliance in PlayerGlobal.Appliances.Values)
        {
            ApplianceID id = appliance.id;
            RectTransform applianceButton = Instantiate(applianceButtonPrefab);
            applianceButton.transform.SetParent(applianceSelectPanel);
            applianceButton.localPosition = new Vector3(xoffset + index%3 * applianceButton.rect.width, yoffset - index/3 * applianceButton.rect.height);
            applianceButton.GetComponent<Button>().onClick.AddListener(() => SelectAppliance(id));
            applianceButton.GetChild(0).GetComponent<Text>().text = appliance.name;
            index++;
        }
    }

    public void UpdateApplianceMenu()
    {
        for (int i = methodPanel.childCount - 1; i >= 0; i--)
        {
            Destroy(methodPanel.GetChild(i).gameObject);
        }
        int index = 0;
        methodPanel.sizeDelta = new Vector2(180f, selectedAppliance.methods.Count * 30f);
        methodPanel.localPosition = new Vector3(0f, methodPanel.rect.height/2 - 40);
        float yoffset = (applianceMenu.rect.height - methodPanel.rect.height) / 2 - 50;
        foreach (ProduceMethod produceMethod in selectedAppliance.methods.Values)
        {
            ProduceMethodID id = produceMethod.id;
            RectTransform method = Instantiate(methodPrefab);
            method.transform.SetParent(methodPanel);
            method.localPosition = new Vector3(0f, -index * 30f + methodPanel.rect.height/2 - 15f, 0f);
            method.name = produceMethod.id.ToString();
            method.GetComponent<Button>().onClick.AddListener(() => SelectMethod(id));
            method.GetChild(0).GetComponent<Text>().text = produceMethod.title;
            index++;
        }
    }

    public void UpdateMethodMaterial()
    {
        for (int i = materialPanelContent.childCount - 1; i >= 0; i--)
        {
            Destroy(materialPanelContent.GetChild(i).gameObject);
        }
        int index = 0;
        float xoffset = -80f;
        float yoffset = 50f;
        materialPanelContent.sizeDelta = new Vector2(220f, selectedProduceMethod.materials.Length * 50f);
        foreach (Item item in selectedProduceMethod.materials)
        {
            RectTransform block = Instantiate(blockPrefab);
            block.transform.SetParent(materialPanelContent);
            block.localScale = Vector3.one;
            block.localPosition = new Vector3(xoffset, -index * 50f + yoffset, 0f);
            int hasCount = (PlayerGlobal.Player != null && PlayerGlobal.Inventory.ContainsKey(item.id)) ? PlayerGlobal.Inventory[item.id].itemCount : 0;
            int needCount = item.itemCount;
            block.GetChild(0).GetComponent<Text>().text = hasCount.ToString();
            block.GetChild(1).GetComponent<Text>().text = item.name.ToString();
            block.GetChild(2).GetComponent<Text>().text = "/" + needCount.ToString();
            index++;
        }
    }

    public void SelectAppliance(ApplianceID id)
    {
        applianceSelectPanel.gameObject.SetActive(false);
        applianceMenu.gameObject.SetActive(true);
        selectedAppliance = PlayerGlobal.Appliances[id];
        applianceNameText.text = selectedAppliance.name;
        var enumerator = selectedAppliance.methods.Values.GetEnumerator();
        enumerator.MoveNext();
        selectedProduceMethod = enumerator.Current;
        UpdateApplianceMenu();
        SelectMethod(selectedProduceMethod.id);
    }

    public void SelectMethod(ProduceMethodID id)
    {
        applianceContent.gameObject.SetActive(true);
        selectedProduceMethod = selectedAppliance.methods[id];
        for (int i = productPanelContent.childCount - 1; i >= 0; i--)
        {
            Destroy(productPanelContent.GetChild(i).gameObject);
        }

        UpdateMethodMaterial();
        int index = 0;
        productPanelContent.sizeDelta = new Vector2(220f, selectedProduceMethod.products.Length * 50f);
        float xoffset = -80f;
        float yoffset = productPanelContent.rect.height / 2 - blockPrefab.rect.height / 2 - 10f;
        foreach (object product in selectedProduceMethod.products)
        {
            if(product is Item)
            {
                Item item = product as Item;
                RectTransform block = Instantiate(blockPrefab);
                block.transform.SetParent(productPanelContent);
                block.localScale = Vector3.one;
                block.localPosition = new Vector3(xoffset, -index * 50f + yoffset, 0f);
                block.GetChild(0).GetComponent<Text>().text = item.itemCount.ToString();
                block.GetChild(1).GetComponent<Text>().text = item.name.ToString();
                index++;
            }
        }

        produceTimeText.text = selectedProduceMethod.processTime.ToString() + "秒";
        processButton.enabled = selectedProduceMethod.Sufficient(PlayerGlobal.Inventory) && !PlayerGlobal.Player.isWorking;
        processButton.image.color = (processButton.enabled)?Color.white:Color.grey;
    }

    public void StartProduce()
    {
        if(selectedAppliance != null && selectedProduceMethod != null)
        {
            if(selectedProduceMethod.Sufficient(PlayerGlobal.Inventory))
            {
                PlayerGlobal.Player.isWorking = true;
                workingAnimation.SetActive(true);
                remainTime = selectedProduceMethod.processTime/10;
                processSlider.maxValue = selectedProduceMethod.processTime/10;
                processSlider.value = selectedProduceMethod.processTime/10;
                PhotonGlobal.PS.Produce(selectedAppliance, selectedProduceMethod);
                processButton.enabled = false;
                processButton.image.color = Color.grey;
            }
        }
    }
    public void ProduceEventAction(bool produceStatus, string debugMessage, ApplianceID applianceID, ProduceMethodID produceMethodID)
    {
        workingAnimation.SetActive(false);
        PlayerGlobal.Player.isWorking = false;
        remainTime = 0;
        if (produceStatus)
        {
            if (PlayerGlobal.Appliances.ContainsKey(applianceID) && PlayerGlobal.Appliances[applianceID].methods.ContainsKey(produceMethodID))
            {
                object[] results;
                selectedAppliance = PlayerGlobal.Appliances[applianceID];
                selectedProduceMethod = selectedAppliance.methods[produceMethodID];
                if (selectedProduceMethod.Process(PlayerGlobal.Inventory, out results))
                {
                    foreach (object result in results)
                    {
                        if (result is Item)
                        {
                            Item item = result as Item;
                            if (PlayerGlobal.Inventory.ContainsKey(item.id))
                            {
                                PlayerGlobal.Inventory[item.id].Increase(item.itemCount);
                            }
                            else
                            {
                                PlayerGlobal.Inventory.Add(item.id, item.Clone() as Item);
                            }
                        }
                        else if (result is Appliance)
                        {
                            Appliance appliance = result as Appliance;
                            if (!PlayerGlobal.Appliances.ContainsKey(appliance.id))
                            {
                                if (selectedAppliance is IUpgradable)
                                {
                                    IUpgradable target = selectedAppliance as IUpgradable;
                                    if (target.UpgradeCheck(appliance))
                                    {
                                        PlayerGlobal.Appliances.Remove(selectedAppliance.id);
                                        Appliance upgraded = target.Upgrade() as Appliance;
                                        PlayerGlobal.Appliances.Add(upgraded.id, upgraded);
                                        SelectAppliance(upgraded.id);
                                    }
                                    else
                                    {
                                        PlayerGlobal.Appliances.Add(appliance.id, appliance);
                                    }
                                }
                                else
                                {
                                    PlayerGlobal.Appliances.Add(appliance.id, appliance);
                                }
                                UpdateApplianceScelectPanel();
                            }
                        }
                    }
                    UpdateMethodMaterial();
                    processButton.enabled = selectedProduceMethod.Sufficient(PlayerGlobal.Inventory);
                    processButton.image.color = (processButton.enabled) ? Color.white : Color.grey;
                    inventoryController.ShowInventory();
                }
            }
        }
        else
        {
            processButton.enabled = selectedProduceMethod.Sufficient(PlayerGlobal.Inventory);
            processButton.image.color = (processButton.enabled) ? Color.white : Color.grey;
        }
    }
}
