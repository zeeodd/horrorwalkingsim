using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFadeOnActive : MonoBehaviour
{
    public Color fadeColor = Color.black;
    public float fadeTime = 3f;

    void Update()
    {
        if (tag == "Main Camera")
        {
            Fade();
        }
    }

    public void Fade()
    {
        CameraFade.StartAlphaFade(fadeColor, true, fadeTime);
    }
}
