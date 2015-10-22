using UnityEngine;

public class LoginController : MonoBehaviour
{
    private string _account = "";
    private string _password = "";

    public void SetAccount(string account)
    {
        _account = account;
    }
    public void SetPassword(string password)
    {
        _password = password;
    }
    public void Login()
    {
        if(_account.Length>0&& _password.Length>0)
            PhotonGlobal.PS.Login(_account, _password);
    }
}
