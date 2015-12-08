using UnityEngine;
using UnityEngine.UI;

public class MoneyPanelController : MonoBehaviour
{
    [SerializeField]
    private Text moneyText;

    void Start()
    {
        GameGlobal.Player.OnMoneyChange += UpdateMoney;
        UpdateMoney();
    }

    void OnDestroy()
    {
        GameGlobal.Player.OnMoneyChange -= UpdateMoney;
    }

    public void UpdateMoney()
    {
        moneyText.text = GameGlobal.Player.money.ToString();
    }

}
