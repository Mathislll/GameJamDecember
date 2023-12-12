using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject pausePanel;
    public bool visible = false;

    public TMP_Dropdown resolutionDropdown;

    private AudioSource audioSourceSFX;
    private AudioSource audioSourceMusic;
    public Slider SFXVolumeSlider;
    public Slider MusicVolumeSlider;
    public TMP_Text SFXTextVolume;
    public TMP_Text MusicTextVolume;
    private SoundManager soundManager;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        if (SoundManager.Instance != null)
        {
            soundManager = SoundManager.Instance;
            audioSourceSFX = soundManager.sfxSource;
            audioSourceMusic = soundManager.musicSource;
        }
        else Debug.LogError("SoundManager is null");

        if (GameManager.Instance != null)
        {
            gameManager = GameManager.Instance;
        }
        else Debug.LogError("GameManager is null");

        SetSFXVolume();
        SetMusicVolume();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Escape"))
        {
            visible = !visible;
            pausePanel.SetActive(visible);
        }
        // stop time
        if (visible)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void Resume()
    {
        visible = false;
        pausePanel.SetActive(visible);
    }

    public void Options()
    {
        pausePanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void Back()
    {
        pausePanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        gameManager.LoadNextLevel("MainMenu");
    }

    public void Restart()
    {
        gameManager.PlayNewGame();
    }

    public void SetResolution()
    {
        switch (resolutionDropdown.value)
        {
            case 0:
                Screen.SetResolution(640, 480, true);
                break;
            case 1:
                Screen.SetResolution(800, 600, true);
                break;
            case 2:
                Screen.SetResolution(1280, 720, true);
                break;
            case 3:
                Screen.SetResolution(1920, 1080, true);
                break;
            default:
                Screen.SetResolution(1920, 1080, true);
                break;
        }
    }

    public void SetSFXVolume()
    {
        audioSourceSFX.volume = SFXVolumeSlider.value;
        SFXTextVolume.text = "SFX Volume " + (audioSourceSFX.volume * 100).ToString("00") + "%";
        
    }
    public void SetMusicVolume()
    {
        audioSourceMusic.volume = MusicVolumeSlider.value;
        MusicTextVolume.text = "Music Volume " + (audioSourceMusic.volume * 100).ToString("00") + "%";

    }
}
