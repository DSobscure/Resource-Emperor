using UnityEngine;
using UnityEngine.UI;
using REStructure;
using REProtocol;
using System.Collections.Generic;
using System.Linq;

public class InventoryPanelController : MonoBehaviour
{
    [SerializeField]
    private RectTransform inventoryPanel;
    [SerializeField]
    private RectTransform inventoryBlockPrefab;
    private RectTransform[] inventoryBlocks;
    internal Dictionary<int,Item> blockPositions;
    private int inventoryRow = 10;
    private int inventoryColumn = 4;
    [SerializeField]
    private float xoffset = -175f;
    [SerializeField]
    private float yoffset = 225f;
    public int selectedItemIndex = -1;
    [SerializeField]
    internal Button discardButton;
    internal Color discardButtonOriginColor;

    void Start ()
    {
        discardButtonOriginColor = discardButton.image.color;
        blockPositions = new Dictionary<int, Item>();
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
        foreach (Item item in GameGlobal.Inventory)
        {
            blockPositions.Add(index, item);
            inventoryBlocks[index].GetChild(0).GetComponent<Text>().text = item.itemCount.ToString();
            inventoryBlocks[index].GetChild(1).GetComponent<Text>().text = item.name.ToString();
            index++;
        }
        GameGlobal.Inventory.OnItemChange += ShowInventory;
    }

    void OnDestroy()
    {
        GameGlobal.Inventory.OnItemChange -= ShowInventory;
    }

    public void UpdateItem(ItemID id)
    {
        if(blockPositions.Any(x=>x.Value.id == id))
        {
            blockPositions.Where(x => x.Value.id == id).ToList().ForEach((pair)=> 
            {
                inventoryBlocks[pair.Key].GetChild(0).GetComponent<Text>().text = pair.Value.itemCount.ToString();
            });
        }
        else if(GameGlobal.Inventory.Any(x=>x.id == id))
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
        foreach (Item item in GameGlobal.Inventory)
        {
            blockPositions.Add(index, item);
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
        if (blockPositions.ContainsKey(selectedItemIndex))
        {
            GameGlobal.Player.SelectedItem = blockPositions.First(x => x.Key == selectedItemIndex).Value;
        }
        inventoryBlocks[selectedItemIndex].GetComponent<Button>().image.color = Color.black;
    }
}
