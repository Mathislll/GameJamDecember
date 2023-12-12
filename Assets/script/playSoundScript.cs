using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playSoundScript : MonoBehaviour
{
    private SoundManager soundManager;
    private AudioSource audioSFXSource;
    public int soundIndex;

    void Start()
    {
        if (SoundManager.Instance != null)
        {
            soundManager = SoundManager.Instance;
            audioSFXSource = soundManager.sfxSource;
        }
        else Debug.LogError("SoundManager is null");
    }

    public void PlaySound()
    {
        audioSFXSource.clip = soundManager.listSound[soundIndex];
        audioSFXSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
