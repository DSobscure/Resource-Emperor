using UnityEngine;
using System.Collections;

public class SceneChangeController : MonoBehaviour
{
    public void ToScene(string sceneName)
    {
        Application.LoadLevel(sceneName);
    }
}
