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

    public float fireRate = 1.0f;  // Délai en secondes entre les tirs
    private float nextFireTime = 0f;  // Temps prochain tir est autorisé

    public bool autoShoot = false;
    public bool isPlayerBullet = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;  // Mise à jour du temps pour le prochain tir
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
    }

    public void Shoot()
    {
        bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletPrefab.transform.rotation, transform.parent);
        bullet.GetComponent<BulletScript>().speed = bulletSpeed;
        bullet.GetComponent<BulletScript>().BulletInit(transform, isPlayerBullet);
    }
}
