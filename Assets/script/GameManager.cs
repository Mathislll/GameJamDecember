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


    public void PlayNewGame()
    {
        SceneManager.LoadScene("Level_01");
    }

    // Update is called once per frame
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
