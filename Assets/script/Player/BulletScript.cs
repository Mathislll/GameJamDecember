using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 50;
    public float lifeTime = 5.0f;
    public Rigidbody rb;

    public bool isPlayerBullet = false;

    public float damage = 1f;
    // Start is called before the first frame update
    void Start()
    {

        //transform.rotation = Quaternion.Euler(0, 0, 90);
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

    public void BulletInit(Transform transform, bool isPlayerBullet)
    {
        this.isPlayerBullet = isPlayerBullet;
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Enemy") && isPlayerBullet)
        {
            collider.gameObject.GetComponent<Enemy_Stat>().TakeHit(damage);
            Destroy(gameObject);
        }
        else if (collider.gameObject.CompareTag("Player") && !isPlayerBullet)
        {
            collider.gameObject.GetComponent<Player_Stat>().TakeHit(damage);
            Destroy(gameObject);
        }

    }
}

