using UnityEngine;
using UnityEngine.UI;

public class MoneyPanelController : MonoBehaviour
{
    [SerializeField]
    private Text moneyText;

    void Start()
    {
        UpdateMoney();
    }

    public void UpdateMoney()
    {
        moneyText.text = GameGlobal.Player.money.ToString();
    }

}
