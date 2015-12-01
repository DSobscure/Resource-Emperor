using UnityEngine;
using System.Collections;
using REStructure.Scenes;

public class LeaveMessageController : MonoBehaviour
{
    [SerializeField]
    private LeaveMessageBoxController leaveMessageBoxController;

    public void LeaveMessage()
    {
        if(GameGlobal.Player.Location is Wilderness)
        {
            Wilderness location = GameGlobal.Player.Location as Wilderness;
            if(leaveMessageBoxController.inputMessage.text.Length > 0)
            {
                leaveMessageBoxController.AddMessage(leaveMessageBoxController.inputMessage.text);
                location.LeaveMessage(leaveMessageBoxController.inputMessage.text);
                PhotonGlobal.PS.LeaveMessage(leaveMessageBoxController.inputMessage.text);
                leaveMessageBoxController.inputMessage.text = "";
            }
        }
    }
}
