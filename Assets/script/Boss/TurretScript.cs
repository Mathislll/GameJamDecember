using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    float dist;
    Transform player;
    public Transform barrel;
    public GameObject bulletPrefab;

    public float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        bulletSpeed = GetComponentInParent<BossShootScript>().turretBulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            dist = Vector3.Distance(player.position, transform.position);
            transform.LookAt(player);
        }

        //if (Input.GetButtonDown("Fire"))
        //{
        //    Shoot();
        //}
    }

    public void Shoot()
    {
        // Calculez la rotation pour que le missile regarde vers le joueur
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle - 270); // Ajustez l'angle si nécessaire

        // Instanciez le missile avec la rotation calculée
        GameObject newBullet = Instantiate(bulletPrefab, barrel.position, rotation);
        newBullet.GetComponent<BulletScript>().BulletInit(transform, false, bulletSpeed);

        //GetComponent<playSoundScript>().PlaySound(); // Décommentez pour jouer le son
    }


    public void SendDamage(int turretNumber)
    {
        transform.GetComponentInParent<BossShootScript>().TakeTurretDamage(turretNumber);
    }
}
