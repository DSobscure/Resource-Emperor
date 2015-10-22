using UnityEngine;
using UnityEngine.UI;
using REStructure;
using REProtocol;
using System.Collections.Generic;
using System.Linq;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private RectTransform inventoryPanel;
    [SerializeField]
    private RectTransform inventoryBlockPrefab;
    private RectTransform[] inventoryBlocks;
    internal Dictionary<ItemID,int> blockPositions;
    private int inventoryRow = 10;
    private int inventoryColumn = 4;
    [SerializeField]
    private float xoffset = -175f;
    [SerializeField]
    private float yoffset = 225f;
    private int selectedItemIndex = -1;
    [SerializeField]
    internal Button discardButton;
    internal Color discardButtonOriginColor;

    void Start ()
    {
        discardButtonOriginColor = discardButton.image.color;
        blockPositions = new Dictionary<ItemID, int>();
        inventoryBlocks = new RectTransform[inventoryRow*inventoryColumn];
        for (int i=0;i<inventoryRow;i++)
        {
            for(int j=0;j<inventoryColumn;j++)
            {
                int blockIndex = i * inventoryColumn + j;
                RectTransform block = Instantiate(inventoryBlockPrefab);
                inventoryBlocks[i*inventoryColumn+j] = block;
                block.transform.SetParent(inventoryPanel);
                block.localScale = Vector3.one;
                block.localPosition = new Vector3(50f*j+ xoffset,-i*50f+ yoffset, 0f);
                block.GetComponent<Button>().onClick.AddListener(() => SelectItem(blockIndex));
            }
        }
        int index = 0;
        foreach (Item item in GameGlobal.Inventory.Values)
        {
            blockPositions.Add(item.id,index);
            inventoryBlocks[index].GetChild(0).GetComponent<Text>().text = item.itemCount.ToString();
            inventoryBlocks[index].GetChild(1).GetComponent<Text>().text = item.name.ToString();
            index++;
        }
    }
    public void UpdateItem(ItemID id)
    {
        if(blockPositions.ContainsKey(id))
        {
            int index = blockPositions[id];
            inventoryBlocks[index].GetChild(0).GetComponent<Text>().text = GameGlobal.Inventory[id].itemCount.ToString();
        }
        else if(GameGlobal.Inventory.ContainsKey(id))
        {
            ShowInventory();
        }
    }
    public void ShowInventory()
    {
        blockPositions.Clear();
        int index = 0;
        for(int i=0;i< inventoryRow; i++)
        {
            for(int j=0;j<inventoryColumn;j++)
            {
                inventoryBlocks[i * inventoryColumn + j].GetChild(0).GetComponent<Text>().text = "";
                inventoryBlocks[i * inventoryColumn + j].GetChild(1).GetComponent<Text>().text = "";
            }
        }
        foreach (Item item in GameGlobal.Inventory.Values)
        {
            blockPositions.Add(item.id, index);
            inventoryBlocks[index].GetChild(0).GetComponent<Text>().text = item.itemCount.ToString();
            inventoryBlocks[index].GetChild(1).GetComponent<Text>().text = item.name.ToString();
            index++;
        }
    }
    public void SelectItem(int index)
    {
        Color originColor = inventoryBlocks[index].GetComponent<Button>().image.color;
        if (selectedItemIndex != -1)
            inventoryBlocks[selectedItemIndex].GetComponent<Button>().image.color = originColor;
        selectedItemIndex = index;
        inventoryBlocks[selectedItemIndex].GetComponent<Button>().image.color = Color.black;
    }
    public void DiscardItem()
    {
        if(blockPositions.ContainsValue(selectedItemIndex))
        {
            var pair = blockPositions.First(x=>x.Value == selectedItemIndex);
            PhotonGlobal.PS.DiscardItem(pair.Key, 1);
            discardButton.enabled = false;
            discardButton.image.color = Color.grey;
        }
    }
}
