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

    private void SendMessageEventAction(string senderName, string message)
    {
        messagePanelController.AppendMessage(senderName + ": " + message);
        messagePanelController.UpdateMessageBox();
    }
}
