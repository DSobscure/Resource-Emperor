using UnityEngine;
using System.Collections;
using REStructure;
using REProtocol;
using UnityEngine.UI;

public class ApplianceContentController : MonoBehaviour
{
    private ProduceMethodDictionary produceMethods;
    public RectTransform materialPanelContent;
    public RectTransform productPanelContent;
    public Text produceTimeText;
    public RectTransform blockPrefab;
    // Use this for initialization
    void Start ()
    {
        produceMethods = new ProduceMethodDictionary();
        ProduceMethod method1 = produceMethods[ProduceMethodID.Iron];

        int index = 0;
        float xoffset = -80f;
        float yoffset = 70f;
        materialPanelContent.sizeDelta = new Vector2(220f, method1.materials.Length * 50f);
        foreach (Item item in method1.materials)
        {
            RectTransform block = Instantiate(blockPrefab);
            block.transform.SetParent(materialPanelContent);
            block.localScale = Vector3.one;
            block.localPosition = new Vector3(xoffset, -index * 50f + yoffset, 0f);
            block.GetChild(0).GetComponent<Text>().text = item.itemCount.ToString();
            block.GetChild(1).GetComponent<Text>().text = item.name.ToString();
            index++;
        }
        index = 0;
        productPanelContent.sizeDelta = new Vector2(220f, method1.products.Length*50f);
        xoffset = -80f;
        yoffset = productPanelContent.rect.height/2 - blockPrefab.rect.height/2 -10f;
        foreach (Item item in method1.products)
        {
            RectTransform block = Instantiate(blockPrefab);
            block.transform.SetParent(productPanelContent);
            block.localScale = Vector3.one;
            block.localPosition = new Vector3(xoffset, -index * 50f + yoffset, 0f);
            block.GetChild(0).GetComponent<Text>().text = item.itemCount.ToString();
            block.GetChild(1).GetComponent<Text>().text = item.name.ToString();
            index++;
        }

        produceTimeText.text = method1.processTime.ToString()+"秒";
    }
}
