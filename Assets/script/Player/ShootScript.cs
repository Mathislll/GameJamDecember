using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public GameObject bullet;

    public float bulletSpeed = 20f;

    public float timer = 0.0f;
    public float timeBetweenShots = 0.1f;

    public float fireRate = 1.0f;  // D�lai en secondes entre les tirs
    private float nextFireTime = 0f;  // Temps prochain tir est autoris�

    public bool autoShoot = false;
    public bool isPlayerBullet = false;

    public bool isCheatMode = false;
    public bool isCheatBullet = false;

    private SoundManager soundManager;
    private AudioSource audioSFXSource;


    // Start is called before the first frame update
    void Start()
    {
        if(SoundManager.Instance != null)
        {
            soundManager = SoundManager.Instance;
            audioSFXSource = soundManager.sfxSource;
        }
        else Debug.LogError("SoundManager is null");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("CheatShoot"))
        {
            isCheatMode = !isCheatMode;
        }

        if (isCheatMode)
        {
            if (Input.GetButtonDown("Fire"))
            {
                isCheatBullet = !isCheatBullet;
            }
        }
        else if (Input.GetButtonDown("Fire") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;  // Mise � jour du temps pour le prochain tir
            Shoot();
        }

        // autoshoot
        if (autoShoot)
        {
            timer += Time.deltaTime;
            if (timer >= timeBetweenShots)
            {

                Shoot();
                timer = 0.0f;

            }
        }

        if (isCheatBullet)
        {
            timer += Time.deltaTime;
            if (timer >= 0.1f)
            {

                Shoot();
                timer = 0.0f;

            }
        }


    }

    public void Shoot()
    {
        bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletPrefab.transform.rotation, transform.parent);
        bullet.GetComponent<BulletScript>().BulletInit(transform, isPlayerBullet, bulletSpeed);
        PlayAttackSound();
    }

    void PlayAttackSound()
    {
        soundManager.PlayShootSound();
    }
}
