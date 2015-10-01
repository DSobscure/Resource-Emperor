using UnityEngine;
using UnityEngine.UI;
using REStructure;
using REProtocol;
using System.Collections.Generic;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private RectTransform inventoryPanel;
    [SerializeField]
    private RectTransform inventoryBlockPrefab;
    private RectTransform[,] inventoryBlocks;
    private Dictionary<ItemID,int> blockPositions;
    private int inventoryRow = 10;
    private int inventoryColumn = 4;
    [SerializeField]
    private float xoffset = -175f;
    [SerializeField]
    private float yoffset = 225f;

	void Start ()
    {
        blockPositions = new Dictionary<ItemID, int>();
        inventoryBlocks = new RectTransform[inventoryRow, inventoryColumn];
        for (int i=0;i<inventoryRow;i++)
        {
            for(int j=0;j<inventoryColumn;j++)
            {
                RectTransform block = Instantiate(inventoryBlockPrefab);
                inventoryBlocks[i, j] = block;
                block.transform.SetParent(inventoryPanel);
                block.localScale = Vector3.one;
                block.localPosition = new Vector3(50f*j+ xoffset,-i*50f+ yoffset, 0f);
            }
        }
        int index = 0;
        foreach (Item item in PlayerGlobal.Player.inventory.Values)
        {
            blockPositions.Add(item.id,index);
            inventoryBlocks[index / inventoryColumn, index % inventoryColumn].GetChild(0).GetComponent<Text>().text = item.itemCount.ToString();
            inventoryBlocks[index / inventoryColumn, index % inventoryColumn].GetChild(1).GetComponent<Text>().text = item.name.ToString();
            index++;
        }
    }

    public void UpdateItem(ItemID id)
    {
        if(blockPositions.ContainsKey(id))
        {
            int index = blockPositions[id];
            inventoryBlocks[index / inventoryColumn, index % inventoryColumn].GetChild(0).GetComponent<Text>().text = PlayerGlobal.Player.inventory[id].itemCount.ToString();
        }
        else if(PlayerGlobal.Player.inventory.ContainsKey(id))
        {
            blockPositions.Clear();
            int index = 0;
            foreach (Item item in PlayerGlobal.Player.inventory.Values)
            {
                blockPositions.Add(item.id, index);
                inventoryBlocks[index / inventoryColumn, index % inventoryColumn].GetChild(0).GetComponent<Text>().text = item.itemCount.ToString();
                inventoryBlocks[index / inventoryColumn, index % inventoryColumn].GetChild(1).GetComponent<Text>().text = item.name.ToString();
                index++;
            }
        }
    }

    public void GameObjectSwitch(GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
