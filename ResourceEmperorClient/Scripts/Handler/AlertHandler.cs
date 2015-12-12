using UnityEngine;
using System.Collections;

public class AlertHandler : MonoBehaviour
{
    [SerializeField]
    private MessageBoxController messageBoxController;

    void Start()
    {
        PhotonGlobal.PS.OnAlert += AlertEventAction;
    }

    void OnDestroy()
    {
        PhotonGlobal.PS.OnAlert -= AlertEventAction;
    }

    private void AlertEventAction(string message)
    {
        messageBoxController.messageBox.SetActive(true);
        messageBoxController.ShowMessage(message);
    }
}
