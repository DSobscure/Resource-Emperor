using UnityEngine;
using System.Collections.Generic;
using System.Text;
using UnityEngine.UI;

public class MessagePanelController : MonoBehaviour
{
    private List<string> messageContent = new List<string>();
    private StringBuilder showingContent = new StringBuilder();

    [SerializeField]
    private RectTransform messageBox;
    [SerializeField]
    private Text showingText;
    //[SerializeField]
    //private Scrollbar scrollbar;
    [SerializeField]
    private InputField inputText;

    public void AppendMessage(string message)
    {
        messageContent.Add(message);
        showingContent.AppendLine(message);
    }

    public void UpdateMessageBox()
    {
        if (messageContent.Count * 20 > 200)
        {
            messageBox.sizeDelta = new Vector2(messageBox.rect.width, messageContent.Count * 20);
            //scrollbar.value = 0;
        }
        showingText.text = showingContent.ToString();
    }

    public void SendMessage()
    {
        if (inputText.text != "")
        {
            PhotonGlobal.PS.SendMessage(inputText.text);
            inputText.text = "";
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(inputText.isFocused)
            {
                SendMessage();
            }
            else
            {
                inputText.ActivateInputField();
                inputText.Select();
            }
        }
    }
}
