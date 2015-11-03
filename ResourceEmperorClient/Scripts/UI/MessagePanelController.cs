using UnityEngine;
using System.Collections.Generic;
using System.Text;
using UnityEngine.UI;

public class MessagePanelController : MonoBehaviour
{
    private List<string> messageContent = new List<string>();
    private StringBuilder showingContent = new StringBuilder();

    [SerializeField]
    private MessageController messageController;
    [SerializeField]
    private RectTransform messageBox;
    [SerializeField]
    private Text showingText;
    public InputField inputText;

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
        }
        showingText.text = showingContent.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(inputText.isFocused)
            {
                messageController.SendMessage();
            }
            else
            {
                inputText.ActivateInputField();
                inputText.Select();
            }
        }
    }
}
