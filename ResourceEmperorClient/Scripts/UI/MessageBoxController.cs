using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MessageBoxController : MonoBehaviour
{
    [SerializeField]
    private Text alertMessage;
    [SerializeField]
    internal GameObject messageBox;

    public void ShowMessage(string message)
    {
        alertMessage.text = message;
    }
}
