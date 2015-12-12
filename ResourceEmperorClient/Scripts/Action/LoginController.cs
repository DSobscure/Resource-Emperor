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
        PhotonGlobal.PS.OnLoginResponse += LoginEventAction;
    }

    void OnDestroy()
    {
        PhotonGlobal.PS.OnLoginResponse -= LoginEventAction;
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

    private void LoginEventAction(bool loginStatus)
    {
        if (loginStatus)
        {
            Application.LoadLevel("WorkRoomScene");
        }
    }
}
