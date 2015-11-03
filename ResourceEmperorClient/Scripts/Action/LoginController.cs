using UnityEngine;
using REStructure;
using REStructure.Scenes;
using System.Collections.Generic;
using REProtocol;
using UnityEngine.UI;

public class LoginController : MonoBehaviour
{
    [SerializeField]
    private Text loginResultText;

    private string _account = "";
    private string _password = "";

    void Start()
    {
        PhotonGlobal.PS.LoginEvent += LoginEventAction;
    }

    void OnDestroy()
    {
        PhotonGlobal.PS.LoginEvent -= LoginEventAction;
    }

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
        if(_account.Length>0&& _password.Length>0)
            PhotonGlobal.PS.Login(_account, _password);
    }

    private void LoginEventAction(bool loginStatus, string debugMessage, Player player, Inventory inventory, Dictionary<ApplianceID, Appliance> appliances)
    {
        if (loginStatus)
        {
            GameGlobal.LoginStatus = true;
            GameGlobal.Player = new ClientPlayer(player);
            GameGlobal.Inventory = inventory;
            GameGlobal.Appliances = appliances;
            GameGlobal.GlobalMap = new GlobalMap();
            GameGlobal.Player.Location = new Room("WorkRoomScene");
            Application.LoadLevel("WorkRoomScene");
        }
        else
        {
            GameGlobal.LoginStatus = false;
            loginResultText.text = debugMessage;
        }
    }
}
