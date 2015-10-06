﻿using UnityEngine;
using System.Collections;
using RESerializable;
using UnityEngine.UI;
using REStructure;

public class LoginController : MonoBehaviour
{
    private string _account = "";
    private string _password = "";
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
        if(_account.Length>0&& _password.Length>0)
            PhotonGlobal.PS.Login(_account, _password);
    }

    void Start()
    {
        PhotonGlobal.PS.LoginEvent += LoginEventAction;
    }

    private void LoginEventAction(bool loginStatus, string debugMessage, SerializablePlayer player, Inventory inventory)
    {
        if (loginStatus)
        {
            PlayerGlobal.LoginStatus = true;
            PlayerGlobal.Player = new Player(player, inventory);
            Application.LoadLevel("WorkRoomScene");
        }
        else
        {
            PlayerGlobal.LoginStatus = false;
            loginResultText.text = debugMessage;
        }
    }
}
