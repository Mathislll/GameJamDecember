using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugBlackScreen : MonoBehaviour
{
    public GameObject fadeManager;
    public Image image;

    public float timeToWait = 3f;
    private float timeElapsed = 0f;

    void Update()
    {
        if (fadeManager.activeSelf && image.color.a == 1) // Vérifie si l'alpha est à 255 (1f en termes de float)
        {
            timeElapsed += 0.01f;
            Debug.Log(timeElapsed);
            if (timeElapsed > timeToWait)
            {
                image.color = new Color(0, 0, 0, 0);
                fadeManager.SetActive(false);
            }
        }

    }

    void SetImageAlphaToZero()
    {
        Debug.Log("SetImageAlphaToZero");
        Color newColor = image.color;
        newColor.a = 0f; // Définit l'alpha à 0
        image.color = newColor;
    }
}
