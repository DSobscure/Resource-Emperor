using UnityEngine;
using System;
using System.Collections.Generic;
using REStructure;
using REStructure.Appliances;
using REProtocol;
using UnityEngine.UI;
using REStructure.Items;

public class ApplianceContentController : MonoBehaviour
{
    private Dictionary<ApplianceID,Appliance> appliances;
    private Appliance selectedAppliance;
    private ProduceMethod selectedProduceMethod;

    [SerializeField]
    private RectTransform applianceSelectPanel;
    [SerializeField]
    private InventoryController inventoryController;
    [SerializeField]
    private RectTransform applianceMenu;
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

    // Use this for initialization
    void Start ()
    {
        appliances = new Dictionary<ApplianceID, Appliance>();
        selectedAppliance = new Machete();
        appliances.Add(selectedAppliance.id, selectedAppliance);

        UpdateApplianceScelectPanel();

        var enumerator = selectedAppliance.methods.Values.GetEnumerator();
        enumerator.MoveNext();
        selectedProduceMethod = enumerator.Current;
        UpdateApplianceMenu();
    }

    public void UpdateApplianceScelectPanel()
    {
        for (int i = applianceSelectPanel.childCount - 1; i >= 0; i--)
        {
            Destroy(applianceSelectPanel.GetChild(i).gameObject);
        }
        int index = 0;
        float xoffset = -300f, yoffset = 200f;
        foreach (Appliance appliance in appliances.Values)
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
        for (int i = applianceMenu.childCount - 1; i > 1; i--)
        {
            Destroy(applianceMenu.GetChild(i).gameObject);
        }
        int index = 0;
        foreach (ProduceMethod produceMethod in selectedAppliance.methods.Values)
        {
            ProduceMethodID id = produceMethod.id;
            RectTransform method = Instantiate(methodPrefab);
            method.transform.SetParent(applianceMenu);
            method.localPosition = new Vector3(0f, -index * 30f + applianceMenu.rect.height / 2 - 45, 0f);
            method.name = produceMethod.id.ToString();
            method.GetComponent<Button>().onClick.AddListener(() => SelectMethod(id));
            method.GetChild(0).GetComponent<Text>().text = produceMethod.title;
            index++;
        }
    }

    public void SelectAppliance(ApplianceID id)
    {
        applianceSelectPanel.gameObject.SetActive(false);
        applianceMenu.gameObject.SetActive(true);
        selectedAppliance = appliances[id];
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
        for (int i = materialPanelContent.childCount - 1; i >= 0; i--)
        {
            Destroy(materialPanelContent.GetChild(i).gameObject);
        }
        for (int i = productPanelContent.childCount - 1; i >= 0; i--)
        {
            Destroy(productPanelContent.GetChild(i).gameObject);
        }

        int index = 0;
        float xoffset = -80f;
        float yoffset = 70f;
        materialPanelContent.sizeDelta = new Vector2(220f, selectedProduceMethod.materials.Length * 50f);
        foreach (Item item in selectedProduceMethod.materials)
        {
            RectTransform block = Instantiate(blockPrefab);
            block.transform.SetParent(materialPanelContent);
            block.localScale = Vector3.one;
            block.localPosition = new Vector3(xoffset, -index * 50f + yoffset, 0f);
            int hasCount = (PlayerGlobal.Player != null && PlayerGlobal.Player.inventory.ContainsKey(item.id))?PlayerGlobal.Player.inventory[item.id].itemCount: 0;
            int needCount = item.itemCount;
            block.GetChild(0).GetComponent<Text>().text = hasCount.ToString() + "/" + needCount.ToString();
            block.GetChild(1).GetComponent<Text>().text = item.name.ToString();
            index++;
        }
        index = 0;
        productPanelContent.sizeDelta = new Vector2(220f, selectedProduceMethod.products.Length * 50f);
        xoffset = -80f;
        yoffset = productPanelContent.rect.height / 2 - blockPrefab.rect.height / 2 - 10f;
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
        processButton.enabled = selectedProduceMethod.Sufficient(PlayerGlobal.Player.inventory);
        processButton.image.color = (processButton.enabled)?Color.white:Color.grey;
    }

    public void StartProduce()
    {
        if(selectedAppliance != null && selectedProduceMethod != null)
        {
            object[] results;
            if(selectedProduceMethod.Process(PlayerGlobal.Player.inventory,out results))
            {
                foreach (object result in results)
                {
                    if(result is Item)
                    {
                        Item item = result as Item;
                        if(PlayerGlobal.Player.inventory.ContainsKey(item.id))
                        {
                            PlayerGlobal.Player.inventory[item.id].Increase(item.itemCount);
                        }
                        else
                        {
                            PlayerGlobal.Player.inventory.Add(item.id, item.Clone() as Item);
                        }
                    }
                    else if(result is Appliance)
                    {
                        Appliance appliance = result as Appliance;
                        if(!appliances.ContainsKey(appliance.id))
                        {
                            appliances.Add(appliance.id,appliance);
                            UpdateApplianceScelectPanel();
                        }
                    }
                }
                SelectMethod(selectedProduceMethod.id);
                inventoryController.ShowInventory();
            }
        }
    }
}
