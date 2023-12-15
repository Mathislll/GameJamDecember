using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playSoundScript : MonoBehaviour
{
    private SoundManager soundManager;
    private AudioSource audioSFXSource;
    private AudioSource audioMusicSource;
    public int soundIndex;

    public string musicToPlay;

    void Start()
    {
        if (SoundManager.Instance != null)
        {
            soundManager = SoundManager.Instance;
            audioSFXSource = soundManager.sfxSource;
            audioMusicSource = soundManager.musicSource;
        }
        else Debug.LogError("SoundManager is null");
    }

    public void PlaySound()
    {
        audioSFXSource.clip = soundManager.listSound[soundIndex];
        audioSFXSource.Play();
    }

    public void PlayMusic()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
