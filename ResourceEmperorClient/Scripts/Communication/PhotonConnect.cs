﻿using UnityEngine;
using System.Collections;

public class PhotonConnect : MonoBehaviour {

    public string ServerIP = "doorofsoul.duckdns.org";
    public int ServerPort = 5055;
    public string ServerName = "REServer";
    private bool ConnectStatus = true;

    void Awake()
    {
        Application.targetFrameRate = 50;
    }

    void Start()
    {
        if (!PhotonGlobal.PS.ServerConnected)
        {
            PhotonGlobal.PS.OnConnectResponse += ConnectEventAction;
            PhotonGlobal.PS.Connect(ServerIP, ServerPort, ServerName);
        }
    }

    private void ConnectEventAction(bool Status)
    {
        if (Status)
        {
            Debug.Log("Connecting . . . . .");
            ConnectStatus = true;
        }
        else
        {
            Debug.Log("Connect Fail");
            ConnectStatus = false;
        }
    }

    private void OnDestroy()
    {
        PhotonGlobal.PS.OnConnectResponse -= ConnectEventAction;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 20), "Version: "+GameGlobal.version);
        if (ConnectStatus == false)
        {
            GUI.Label(new Rect(130, 10, 100, 20), "Connect fail");
        }

        if (PhotonGlobal.PS.ServerConnected)
        {
            GUI.Label(new Rect(130, 10, 100, 20), "Connecting . . .");
        }
    }
}
