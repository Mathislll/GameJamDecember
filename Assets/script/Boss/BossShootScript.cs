using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShootScript : MonoBehaviour
{
#pragma warning disable CS0414
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public GameObject bullet;

    public GameObject turret1;
    public GameObject turret2;
    public GameObject eyeBody;

    public float timer = 0.0f;
    public float timeBetweenShots = 0.1f;

    public float fireRate = 1.0f;  // D�lai en secondes entre les tirs
    private float nextFireTime = 0f;  // Temps prochain tir est autoris�

    public bool canShoot = false; // autorise a tirer quand entr� dans le niveau
    public bool autoShoot = false;
    public bool isPlayerBullet = false;

    [Header("Big Shoot parameter")]
    public float bulletSpeed = 5f;
    public float bulletLifeTime = 2f;
    public int numberOfBursts = 5; // Nombre de salves de balles
    public float delayBetweenBursts = 0.5f; // D�lai entre les salves
    public float yOffset = 0.5f; // D�calage vertical entre chaque balle
    public float timerBigShoot = 0.0f;
    public float timeBetweenBigShoot = 5f;

    [Header("Turret parameter")]
    public float turretBulletSpeed = 4f;
    public float turret1Timer = 0.0f;
    public float turret1TimeBetweenShots = 3f;
    public float turret2Timer = 0.0f;
    public float turret2TimeBetweenShots = 3f;
    public float delayBetweenTurret = 2f;
    public float turret1Health = 2f;
    public float turret2Health = 2f;

    private GameManager gameManager;

    public void TakeTurretDamage(int turretNumber)
    {        
        if (turretNumber == 1)
        {
            turret1Health--;
            if (turret1Health <= 0)
            {
                // d�truire la tourelle
                GetComponent<Enemy_Stat>().isTurret1Dead = true;
                gameManager.AddScore(20);
                gameManager.CallUpdateScore();
                Destroy(turret1);
            }
        }
        else if (turretNumber == 2)
        {
            turret2Health--;
            if (turret2Health <= 0)
            {
                GetComponent<Enemy_Stat>().isTurret2Dead = true;
                gameManager.AddScore(20);
                gameManager.CallUpdateScore();
                Destroy(turret2);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.Instance != null)
        {
            gameManager = GameManager.Instance;
        }
        else
        {
            Debug.LogError("GameManager is null");
        }
        // initialiser le d�calage entre les tir turret
        turret2Timer += delayBetweenTurret;
    }

    // Update is called once per frame
    void Update()
    {
        // TO DEBUG
        //if (Input.GetButtonDown("Fire") && Time.time >= nextFireTime)
        //{
        //    nextFireTime = Time.time + fireRate;  // Mise � jour du temps pour le prochain tir
        //    StartBigShoot();
        //    turret1.GetComponent<TurretScript>().Shoot();
        //    turret2.GetComponent<TurretScript>().Shoot();
        //}

        // autoshoot
        if(canShoot)
        {
            if (autoShoot)
            {
                timer += Time.deltaTime;
                if (timer >= timeBetweenShots)
                {

                    ShootVerticalGroup();
                    timer = 0.0f;

                }
            }

            // big Shoot
            if (eyeBody != null)
            {
                timerBigShoot += Time.deltaTime;
                if (timerBigShoot >= timeBetweenBigShoot)
                {
                    StartBigShoot();
                    timerBigShoot = 0.0f;
                }
            }

            // turret shoot
            if (turret1 != null)
            {
                turret1Timer += Time.deltaTime;
                if (turret1Timer >= turret1TimeBetweenShots)
                {
                    turret1.GetComponent<TurretScript>().Shoot();
                    turret1Timer = 0.0f;
                }
            }

            if (turret2 != null)
            {
                turret2Timer += Time.deltaTime;
                if (turret2Timer >= turret2TimeBetweenShots)
                {
                    turret2.GetComponent<TurretScript>().Shoot();
                    turret2Timer = 0.0f;
                }
            }
        }
    }

    public void Shoot()
    {
        float yOffset = 0.5f; // D�calage vertical entre chaque balle

        for (int i = 0; i < 3; i++)
        {
            GetComponent<playSoundScript>().PlaySound();
            // Calculez la position Y pour chaque balle
            Vector3 bulletPosition = bulletSpawn.position + new Vector3(0, i * yOffset - yOffset, 0);

            // Instanciez la balle
            GameObject newBullet = Instantiate(bulletPrefab, bulletPosition, bulletPrefab.transform.rotation, transform.parent);
            newBullet.GetComponent<BulletScript>().BulletInit(transform, isPlayerBullet, bulletSpeed);
        }
    }



    public void StartBigShoot()
    {
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
        Quaternion rotation = Quaternion.Euler(0, 0, -90); // Rotation de -90 degr�s sur l'axe Z

        for (int i = 0; i < 3; i++)
        {
            Vector3 bulletPosition = bulletSpawn.position + new Vector3(0, i * yOffset - yOffset, 0);
            GameObject newBullet = Instantiate(bulletPrefab, bulletPosition, rotation); // Utilisez la rotation d�finie
            if (newBullet.GetComponent<BulletScript>() != null)
            {
                if (eyeBody != null)
                {
                    newBullet.GetComponent<BulletScript>().BulletInit(eyeBody.transform, isPlayerBullet, bulletSpeed);
                }
            }
        }
    }


    public void SetCanOpenFire(bool set)
    {
        canShoot = set;
    }
}
