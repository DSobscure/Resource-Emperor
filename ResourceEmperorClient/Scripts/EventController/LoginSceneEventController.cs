using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using REStructure;
using REStructure.Scenes;
using REProtocol;
using System;

public class LoginSceneEventController : MonoBehaviour, ISceneEventController
{
    [SerializeField]
    private Text loginResultText;

    void Start ()
    {
        InitialEvents();
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

            ClearEvents();
            Application.LoadLevel("WorkRoomScene");
        }
        else
        {
            GameGlobal.LoginStatus = false;
            loginResultText.text = debugMessage;
        }
    }

    public void InitialEvents()
    {
        PhotonGlobal.PS.LoginEvent += LoginEventAction;
    }

    public void ClearEvents()
    {
        PhotonGlobal.PS.LoginEvent -= LoginEventAction;
    }
}
