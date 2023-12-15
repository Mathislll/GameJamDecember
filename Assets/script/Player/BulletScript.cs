using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float lifeTime = 5.0f;
    public Rigidbody rb;

    public bool isPlayerBullet = false;

    public float damage = 1f;

    public ParticleSystem explosionParticule1;
    public ParticleSystem explosionParticule2;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {

        //transform.rotation = Quaternion.Euler(0, 0, 90);
    }

    public void SpawnAudioBullet()
    {
        Instantiate(audioSource, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void BulletInit(Transform transform, bool isPlayerBullet, float speed)
    {
        this.isPlayerBullet = isPlayerBullet;
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

    public void PlayExplosionParticule()
    {
        Instantiate(explosionParticule1, transform.position, Quaternion.identity);
        Instantiate(explosionParticule2, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Enemy") && isPlayerBullet)
        {
            if (collider.gameObject.GetComponent<Enemy_Stat>() != null)
            {
                collider.gameObject.GetComponent<Enemy_Stat>().TakeHit(damage);
                PlayExplosionParticule();
                SpawnAudioBullet();
                Destroy(gameObject);

            }
        }
        else if (collider.gameObject.CompareTag("Player") && !isPlayerBullet)
        {
            collider.gameObject.GetComponent<Player_Stat>().TakeHit(damage);
            PlayExplosionParticule();
            SpawnAudioBullet();


            Destroy(gameObject);
        }
        else if (collider.gameObject.CompareTag("Bomb"))
        {
            collider.GetComponent<bombaScript>().MacronExplosion();
            PlayExplosionParticule();
            SpawnAudioBullet();
            Destroy(gameObject);
        }
        else if(collider.gameObject.CompareTag("Turret"))
        {             
            collider.GetComponent<TurretScript>().SendDamage(1);
            PlayExplosionParticule();
            SpawnAudioBullet();

            Destroy(gameObject);
        }
        else if (collider.gameObject.CompareTag("Turret2"))
        {
            collider.GetComponent<TurretScript>().SendDamage(2);
            PlayExplosionParticule();
            SpawnAudioBullet();

            Destroy(gameObject);
        }
        else if (collider.gameObject.CompareTag("Projectile"))
        {
            PlayExplosionParticule();
            Destroy(collider.gameObject);
            SpawnAudioBullet();

            Destroy(gameObject);
        }
        else if (collider.gameObject.CompareTag("Boss"))
        {
            SpawnAudioBullet();

            Destroy(gameObject);
        }
        else if (collider.gameObject.CompareTag("obstacle"))
        {
            SpawnAudioBullet();

            PlayExplosionParticule();
            Destroy(gameObject);
        }

    }
}

