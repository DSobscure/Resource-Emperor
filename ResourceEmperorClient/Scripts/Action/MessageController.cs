using UnityEngine;
using System.Collections;

public class MessageController : MonoBehaviour
{
    [SerializeField]
    private MessagePanelController messagePanelController;

    void Start()
    {
        PhotonGlobal.PS.OnSendMessageResponse += SendMessageEventAction;
    }

    void OnDestroy()
    {
        PhotonGlobal.PS.OnSendMessageResponse -= SendMessageEventAction;
    }

    public void SendMessage()
    {
        if (messagePanelController.inputText.text != "")
        {
            PhotonGlobal.PS.SendMessage(messagePanelController.inputText.text);
            messagePanelController.inputText.text = "";
        }
    }

    private void SendMessageEventAction(bool status, string senderName, string message)
    {
        if(status)
        {
            messagePanelController.AppendMessage(senderName + ": " + message);
            messagePanelController.UpdateMessageBox();
        }
    }
}
