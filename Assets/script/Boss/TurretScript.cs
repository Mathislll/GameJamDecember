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
        Quaternion rotation = Quaternion.Euler(0, 0, -90);

        //GetComponent<playSoundScript>().PlaySound();
        GameObject newBullet = Instantiate(bulletPrefab, barrel.position, rotation);
        newBullet.GetComponent<BulletScript>().BulletInit(barrel, false, bulletSpeed);
    }

    public void SendDamage(int turretNumber)
    {
        transform.GetComponentInParent<BossShootScript>().TakeTurretDamage(turretNumber);
    }
}
