using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenTimer : MonoBehaviour
{

    public float timeToWait = 5f;
    public float timeElapsed = 0f;
    public string levelToLoad;
    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.Instance != null)
        {
            gameManager = GameManager.Instance;
        }
        Time.timeScale = 1f;


    }

    // Update is called once per frame
    void Update()
    {
        
        timeElapsed += Time.deltaTime;
        if(timeElapsed > timeToWait)
        {
           timeElapsed = 0f;
            gameManager.LoadNextLevel(levelToLoad);
        }
    }
}
