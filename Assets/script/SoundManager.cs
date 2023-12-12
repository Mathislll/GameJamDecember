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
    public AudioClip characterDie;
    [Header("CharacterShoot")]
    public AudioClip[] listCharacterShoot;

    [Header("Jumping sound")]
    public AudioClip[] listJumpingSound;

    [Header("Sound")]
    public AudioClip[] listSound;



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

    public void PlayJumpingSound()
    {
        sfxSource.PlayOneShot(listJumpingSound[Random.Range(0, listJumpingSound.Length)]);
    }

    public void PlayShootSound()
    {
        sfxSource.PlayOneShot(listCharacterShoot[Random.Range(0, listCharacterShoot.Length)]);
    }
}
