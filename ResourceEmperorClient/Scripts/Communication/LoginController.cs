using UnityEngine;
using System.Collections;
using RESerializable;
using UnityEngine.UI;

public class LoginController : MonoBehaviour
{
    private string _account;
    private string _password;
    [SerializeField]
    private Text loginResultText;

    public void SetAccount(string account)
    {
        _account = account;
    }
    public void SetPassword(string password)
    {
        _password = password;
    }
    public void Login()
    {
        PhotonGlobal.PS.Login(_account, _password);
    }

    void Start()
    {
        PhotonGlobal.PS.LoginEvent += LoginEventAction;
    }

    private void LoginEventAction(bool loginStatus, string debugMessage, SerializablePlayer player, string inventoryDataString)
    {
        if (loginStatus)
        {
            PlayerGlobal.LoginStatus = true;
            PlayerGlobal.Player = new Player(player);
            PlayerGlobal.Player.inventory.StringDeserialize(inventoryDataString);
            Application.LoadLevel("WorkRoomScene");
        }
        else
        {
            PlayerGlobal.LoginStatus = false;
            loginResultText.text = debugMessage;
        }
    }
}
