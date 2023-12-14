using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionBarScript : MonoBehaviour
{

    //public Image timerBar;
    public Slider slider;

    private float timerDuration;

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
            //UpdateBarFillAmount();
            UpdateSliderAmount();
            //Debug.Log(timerElapsed.ToString());
        }
    }

    //void UpdateBarFillAmount()
    //{
    //    // Calculez le pourcentage de temps écoulé et mettez à jour fillAmount
    //    float fillAmount = timerElapsed / timerDuration;
    //    timerBar.fillAmount = fillAmount;
    //}

    public void UpdateSliderAmount()
    {
        slider.value = timerElapsed / timerDuration;
    }

    public void SetTimerDuration(float set)
    {
        timerDuration = set;
        //Debug.Log(timerDuration.ToString());
    }
}
