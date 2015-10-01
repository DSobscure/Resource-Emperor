using UnityEngine;
using System.Collections;
using REProtocol;
using REStructure.Items.Materials;
using UnityEngine.UI;

public class CollectMaterial : MonoBehaviour
{
    public InventoryController inventoryController;
    [SerializeField]
    private Slider slider;

    private bool workingStatus = false;
    public void WorkingSwitch()
    {
        workingStatus = !workingStatus;
        if (workingStatus)
        {
            slider.value = 0;
            slider.gameObject.SetActive(true);
            StartCoroutine("GetMaterial");
        }
        else
        {
            slider.value = 0;
            slider.gameObject.SetActive(false);
            StopCoroutine("GetMaterial");
        }
    }

    IEnumerator GetMaterial()
    {
        slider.value += 0.05f;
        if(slider.value >= 5f)
        {
            if (PlayerGlobal.Player.inventory.ContainsKey(ItemID.Log))
            {
                PlayerGlobal.Player.inventory[ItemID.Log].Increase();
            }
            else
            {
                PlayerGlobal.Player.inventory.Add(ItemID.Log, new Log(1));
            }
            inventoryController.UpdateItem(ItemID.Log);
            slider.value = 0;
        }
        yield return new WaitForSeconds(0.05f);
        StartCoroutine("GetMaterial");
    }
}
