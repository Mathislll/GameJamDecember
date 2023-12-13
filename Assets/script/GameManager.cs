using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int playerHeatlh = 10;

    public bool debugMode = false;

    public bool isFinalBossDefeated = false;

    public int actualScore = 0;
    public int bestScore = 0;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (debugMode)
        {
            PlayNewGame();
        }
    }

    public void AddScore(int scoreToAdd)
    {
        actualScore += scoreToAdd;
        if (actualScore > bestScore)
        {
            bestScore = actualScore;
        }
    }

    public void CallUpdateScore()
    {
        // find the score system
        GameObject scoreSystem = GameObject.Find("ScoreSystem");
        if (scoreSystem != null)
        {
            ScoreSystem scoreSystemScript = scoreSystem.GetComponent<ScoreSystem>();
            if (scoreSystemScript != null)
            {
                scoreSystemScript.UpdateScore();
            }
            else Debug.LogError("ScoreSystem script is null");
        }
        else Debug.LogError("ScoreSystem is null");
    }

    public void PlayNewGame()
    {
        actualScore = 0;
        SceneManager.LoadScene("Level_01");
    }

    void Update()
    {

    }

    public void LoadNextLevel(string nextLevelName)
    {
        SceneManager.LoadScene(nextLevelName);
    }

    public void FinishTheGame()
    {
        LoadNextLevel("MainMenu");
    }
}
