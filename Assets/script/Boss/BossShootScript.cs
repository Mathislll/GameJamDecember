using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShootScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public GameObject bullet;

    public float timer = 0.0f;
    public float timeBetweenShots = 0.1f;

    public float fireRate = 1.0f;  // Délai en secondes entre les tirs
    private float nextFireTime = 0f;  // Temps prochain tir est autorisé

    public bool autoShoot = false;
    public bool isPlayerBullet = false;

    [Header("Shoot parameter")]
    public float bulletSpeed = 5f;
    public float bulletLifeTime = 2f;
    public int numberOfBursts = 5; // Nombre de salves de balles
    public float delayBetweenBursts = 0.5f; // Délai entre les salves
    public float yOffset = 0.5f; // Décalage vertical entre chaque balle



    // Start is called before the first frame update
    void Start()
    {
        bulletPrefab.GetComponent<BulletScript>().speed = bulletSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;  // Mise à jour du temps pour le prochain tir
            StartShooting();
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
        float yOffset = 0.5f; // Décalage vertical entre chaque balle

        for (int i = 0; i < 3; i++)
        {
            // Calculez la position Y pour chaque balle
            Vector3 bulletPosition = bulletSpawn.position + new Vector3(0, i * yOffset - yOffset, 0);

            // Instanciez la balle
            GameObject newBullet = Instantiate(bulletPrefab, bulletPosition, bulletPrefab.transform.rotation, transform.parent);
            newBullet.GetComponent<BulletScript>().BulletInit(transform, isPlayerBullet);
        }
    }

    public void StartShooting()
    {
        bulletPrefab.GetComponent<BulletScript>().speed = bulletSpeed;
        bulletPrefab.GetComponent<BulletScript>().lifeTime = bulletLifeTime;
        StartCoroutine(ShootBurst());
    }

    IEnumerator ShootBurst()
    {

        for (int burst = 0; burst < numberOfBursts; burst++)
        {
            ShootVerticalGroup(); // Tire un groupe de balles
            yield return new WaitForSeconds(delayBetweenBursts);
        }
    }

    void ShootVerticalGroup()
    {

        for (int i = 0; i < 3; i++)
        {
            Vector3 bulletPosition = bulletSpawn.position + new Vector3(0, i * yOffset - yOffset, 0);
            GameObject newBullet = Instantiate(bulletPrefab, bulletPosition, bulletPrefab.transform.rotation, transform.parent);
            newBullet.GetComponent<BulletScript>().BulletInit(transform, isPlayerBullet);
        }
    }


}
