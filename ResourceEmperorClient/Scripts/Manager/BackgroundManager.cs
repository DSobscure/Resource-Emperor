using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField]
    private Image backgroundImage;

    // Use this for initialization
    void Start()
    {
        Sprite background = Resources.Load(GameGlobal.Player.Location.name, typeof(Sprite)) as Sprite;
        if (background != null)
            backgroundImage.sprite = background;
    }
}
