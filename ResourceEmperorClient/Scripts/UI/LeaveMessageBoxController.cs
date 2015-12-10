using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using REStructure.Scenes;

public class LeaveMessageBoxController : MonoBehaviour
{
    [SerializeField]
    internal Text messages;
    [SerializeField]
    internal InputField inputMessage;
    private StringBuilder messageContent;
    [SerializeField]
    private Scrollbar scrollBar;

    void Start ()
    {
        messageContent = new StringBuilder();
        Wilderness location = GameGlobal.Player.Location as Wilderness;
        foreach (string message in location.messages)
            messageContent.AppendLine(message);
        messages.text = messageContent.ToString();
    }

    public void AddMessage(string message)
    {
        messageContent.AppendLine(message);
        messages.text = messageContent.ToString();
        messages.rectTransform.sizeDelta = new Vector2(messages.rectTransform.rect.width, messages.preferredHeight);
        scrollBar.value = 0;
    }
}
