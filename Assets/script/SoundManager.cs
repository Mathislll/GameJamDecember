using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [Header("Audio sources")]

    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Character sound")]
    public AudioClip characterAttack01;

    [Header("Item sound")]
    // faire une liste de tous les sons d'items
    public AudioClip[] listItemSound;

    [Header("Foot sound")]
    public AudioClip[] listFootSound;

    [Header("Landing sound")]
    public AudioClip[] listLandingSound;

    [Header("Dashing sound")]
    public AudioClip[] listDashingSound;

    [Header("Jumping sound")]
    public AudioClip[] listJumpingSound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayFootStep()
    {
        sfxSource.PlayOneShot(listFootSound[Random.Range(0, listFootSound.Length)]);
    }

    public void PlayLandingSound()
    {
        sfxSource.PlayOneShot(listLandingSound[Random.Range(0, listLandingSound.Length)]);
    }

    public void PlayDashingSound()
    {
        sfxSource.PlayOneShot(listDashingSound[Random.Range(0, listDashingSound.Length)]);
    }

    public void PlayJumpingSound()
    {
        sfxSource.PlayOneShot(listJumpingSound[Random.Range(0, listJumpingSound.Length)]);
    }
}
