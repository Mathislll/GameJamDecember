using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionBarScript : MonoBehaviour
{

    public Image timerBar;

    public float timerDuration = 10.0f; 

    private float timerElapsed = 0.0f; // temps ecoulé

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timerElapsed < timerDuration)
        {
            timerElapsed += Time.deltaTime;
            UpdateBarFillAmount();
        }
    }

    void UpdateBarFillAmount()
    {
        // Calculez le pourcentage de temps écoulé et mettez à jour fillAmount
        float fillAmount = timerElapsed / timerDuration;
        timerBar.fillAmount = fillAmount;
    }
}
