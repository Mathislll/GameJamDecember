using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stat : MonoBehaviour
{
    public float health = 3f;
    public Transform respawnPoint;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeHit(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            Respawn();
        }
    }

    public void TakeHeal(float heal)
    {
        health += heal;
    }

    public void Respawn()
    {
        transform.position = respawnPoint.position;
    }

}
