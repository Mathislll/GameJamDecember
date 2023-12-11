using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    public float timer = 0.0f;
    public float timeBetweenShots = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown ("Fire") == true)
        {
            Shoot();
        }
        
        timer += Time.deltaTime;
        if (timer >= timeBetweenShots)
        {
            
                Shoot();
                timer = 0.0f;
            
        }
    }

    public void Shoot()
    {
        Instantiate(bulletPrefab, bulletSpawn.position, bulletPrefab.transform.rotation);
    }
}
