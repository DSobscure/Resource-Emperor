using UnityEngine;
using System.Collections;
using REStructure;
using REProtocol;
using System.Linq;
using REStructure.Items;
using UnityEngine.UI;
using System;

public class MarketPanelController : MonoBehaviour
{
    private Market market;
    [SerializeField]
    private RectTransform marketContentPanel;
    [SerializeField]
    private RectTransform commodityUIPrefab;
    private Type selectedType = typeof(REStructure.Items.Material);
    [SerializeField]
    private Dropdown selectedTypeDropdown;
    [SerializeField]
    private Scrollbar commodityScrollbar;

    // Use this for initialization
    void Start ()
    {
        market = GameGlobal.GlobalMap.towns[0].market;
        market.OnCommodityChange += UpdateMarketPanel;
        UpdateMarketPanel();
    }

    void OnDestroy()
    {
        market.OnCommodityChange -= UpdateMarketPanel;
    }

    void UpdateMarketPanel()
    {
        for (int i = marketContentPanel.childCount - 1; i >= 0; i--)
        {
            Destroy(marketContentPanel.GetChild(i).gameObject);
        }
        int index = 0;
        marketContentPanel.sizeDelta = new Vector2(marketContentPanel.rect.width, market.catalog.Count(commodity => selectedType.IsInstanceOfType(commodity.item)) * (commodityUIPrefab.rect.height+10f) + 10f);
        Vector2 commodityUIOffset = new Vector2(-5f, marketContentPanel.rect.height/2 - commodityUIPrefab.rect.height/2 -10f);
        foreach (Commodity commodity in market.catalog)
        {
            if(selectedType.IsInstanceOfType(commodity.item))
            {
                ItemID targetID = commodity.item.id;
                RectTransform commodityUI = Instantiate(commodityUIPrefab);
                commodityUI.transform.SetParent(marketContentPanel);
                commodityUI.localScale = Vector3.one;
                commodityUI.localPosition = commodityUIOffset + new Vector2(0, - index * (commodityUI.rect.height+10f));
                commodityUI.GetChild(0).GetComponent<Text>().text = commodity.item.name;
                commodityUI.GetChild(2).GetComponent<Text>().text = commodity.stock.ToString() + "/" + commodity.maxStock.ToString();
                commodityUI.GetChild(4).GetComponent<Text>().text = commodity.price.ToString();
                commodityUI.GetChild(5).GetComponent<Button>().onClick.AddListener(()=> PhotonGlobal.PS.PurchaseCommodity(targetID, 1));
                commodityUI.GetChild(6).GetComponent<Button>().onClick.AddListener(() => PhotonGlobal.PS.SellCommodity(targetID, 1));
                index++;
            }
        }
        commodityScrollbar.value = 1;
    }

    public void SelectCommodityType()
    {
        switch(selectedTypeDropdown.value)
        {
            case 0:
                selectedType = typeof(REStructure.Items.Material);
                break;
            case 1:
                selectedType = typeof(REStructure.Items.Product);
                break;
        }
        UpdateMarketPanel();
    }
}
