using UnityEngine;
using System.Collections;

public class MessageController : MonoBehaviour
{
    [SerializeField]
    private MessagePanelController messagePanelController;

    void Start()
    {
        PhotonGlobal.PS.SendMessageEvent += SendMessageEventAction;
    }

    void OnDestroy()
    {
        PhotonGlobal.PS.SendMessageEvent -= SendMessageEventAction;
    }

    public void SendMessage()
    {
        if (messagePanelController.inputText.text != "")
        {
            PhotonGlobal.PS.SendMessage(messagePanelController.inputText.text);
            messagePanelController.inputText.text = "";
        }
    }

    private void SendMessageEventAction(bool status, string debugMessage, string senderName, string message)
    {
        if(status)
        {
            messagePanelController.AppendMessage(senderName + ": " + message);
            messagePanelController.UpdateMessageBox();
        }
        else
        {
            Debug.Log(debugMessage);
        }
    }
}
