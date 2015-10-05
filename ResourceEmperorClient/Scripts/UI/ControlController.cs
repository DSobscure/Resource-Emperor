using UnityEngine;
using System.Collections;

public class ControlController : MonoBehaviour
{
    public void GameObjectSwitch(GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
