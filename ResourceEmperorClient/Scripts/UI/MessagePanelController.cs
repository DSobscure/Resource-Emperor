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
    [SerializeField]
    private Scrollbar scrollBar;

    public void AppendMessage(string message)
    {
        messageContent.Add(message);
        showingContent.AppendLine(message);
    }

    public void UpdateMessageBox()
    {
        showingText.text = showingContent.ToString();
        showingText.rectTransform.sizeDelta = new Vector2(showingText.rectTransform.rect.width, showingText.preferredHeight);
        scrollBar.value = 0;
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
