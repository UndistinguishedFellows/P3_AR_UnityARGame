using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AlphaButton : MonoBehaviour
{
    private Image img;

    void Start()
    {
        img = GetComponent<Image>();
    }

    void OnClick()
    {
        Color color = img.color;
        color.a = 200;
        img.color = color;
    }
}
