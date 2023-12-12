using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    float dist;
    Transform player;
    public Transform head, barrel;
    public GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(player.position, transform.position);
        head.LookAt(player);

        if (Input.GetButtonDown("Fire"))
        {
            Shoot();
        }
    }

    public void Shoot()
    {

        GameObject newBullet = Instantiate(bulletPrefab, barrel.position, transform.rotation);
        newBullet.GetComponent<BulletScript>().BulletInit(barrel, false);
    }
}
