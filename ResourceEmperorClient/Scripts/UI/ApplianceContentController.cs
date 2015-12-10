using UnityEngine;
using REStructure;
using REProtocol;
using UnityEngine.UI;
using System;
using System.Linq;

public class ApplianceContentController : MonoBehaviour
{
    internal float remainTime;
    internal Appliance selectedAppliance;
    internal ProduceMethod selectedProduceMethod;

    //panels
    [SerializeField]
    private RectTransform applianceSelectPanel;
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

    //texts
    [SerializeField]
    private Text applianceNameText;
    [SerializeField]
    private Text produceTimeText;

    //buttons
    [SerializeField]
    internal Button processButton;
    [SerializeField]
    internal Button cancelButton;

    //sliders
    [SerializeField]
    internal Slider processSlider;

    //animations
    [SerializeField]
    internal GameObject workingAnimation;

    //prefabs
    [SerializeField]
    private RectTransform blockPrefab;
    [SerializeField]
    private RectTransform methodPrefab;
    [SerializeField]
    private RectTransform applianceButtonPrefab;

    void Start ()
    {
        UpdateApplianceScelectPanel();
    }
    void Update()
    {
        if(GameGlobal.Player != null && GameGlobal.Player.IsWorking)
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
        foreach (Appliance appliance in GameGlobal.Appliances.Values)
        {
            ApplianceID id = appliance.id;
            RectTransform applianceButton = Instantiate(applianceButtonPrefab);
            applianceButton.transform.SetParent(applianceSelectPanel);
            applianceButton.localScale = Vector3.one;
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
            method.localScale = Vector3.one;
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
            int itemCount = (GameGlobal.Player != null && GameGlobal.Inventory.Any(x=>x.id == item.id)) ? GameGlobal.Inventory.Where(x => x.id == item.id).Sum(x => x.itemCount) : 0;
            int needCount = item.itemCount;
            block.GetChild(0).GetComponent<Text>().text = itemCount.ToString();
            block.GetChild(1).GetComponent<Text>().text = item.name.ToString();
            block.GetChild(2).GetComponent<Text>().text = "/" + needCount.ToString();
            index++;
        }
    }
    public void SelectAppliance(ApplianceID id)
    {
        applianceSelectPanel.gameObject.SetActive(false);
        applianceMenu.gameObject.SetActive(true);
        selectedAppliance = GameGlobal.Appliances[id];
        applianceNameText.text = selectedAppliance.name;
        UpdateApplianceMenu();
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
        processButton.enabled = !GameGlobal.Player.IsWorking && selectedProduceMethod.Sufficient(GameGlobal.Inventory);
        processButton.image.color = (processButton.enabled)?Color.white:Color.grey;
    }
}
