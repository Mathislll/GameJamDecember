using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    private SoundManager soundManager;
    // Start is called before the first frame update
    public void PlayNewGame()
    {
        GameManager.Instance.isGameFinished = false;
        GameManager.Instance.isFinalBossDefeated = false;
        GameManager.Instance.LoadNextLevel("LoadingScreen");
    }

    public void PlayEndlessMode()
    {
        GameManager.Instance.isGameFinished = false;
        GameManager.Instance.isFinalBossDefeated = false;
        GameManager.Instance.LoadNextLevel("Level_Endless");
    }

    private void Start()
    {
        if (SoundManager.Instance != null)
        {
            soundManager = SoundManager.Instance;
            soundManager.PlayMenuMusic();
        }
        else Debug.LogError("SoundManager is null");
        Time.timeScale = 1f;
    }



    public void PlayMathisLevel()
    {
        SceneManager.LoadScene("Level_Mathis");
    }

    public void PlayCedricLevel()
    {
        SceneManager.LoadScene("Level_Cedric");
    }

    public void QuitGame()
    {
        Application.Quit();

    }
}
