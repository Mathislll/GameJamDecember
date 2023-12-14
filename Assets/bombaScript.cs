using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombaScript : MonoBehaviour
{
    private float speed;
    [SerializeField]
    public float positionSpawnY;
    [SerializeField]
    private GameObject bubulParticul;
    [SerializeField]
    private GameObject boomParticul;
    [SerializeField]
    private GameObject obstacleToDestroy;
    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private bool trapMove = false;

    private const float spawnPositionX = 30;

    void Start()
    {
        speed = speed * -1f;
    }

    void Update()
    {
        if (transform.position.x < -20)
        {
            Destroy(gameObject);
        }

        if (trapMove)
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }

    }

    public void MacronExplosion()
    {
        GameObject bubul = Instantiate(bubulParticul,this.transform);
        GameObject boom = Instantiate(boomParticul, this.transform);
        bubul.transform.parent = null;
        boom.transform.parent = null;
        if (obstacleToDestroy != null )
        {
            Destroy(obstacleToDestroy.gameObject);
        }
        GetComponent<playSoundScript>().PlaySound();
        Destroy(this.gameObject);
    }

    public void SetIsMove()
    {
        trapMove = true;
        this.transform.position = new Vector3(spawnPositionX, positionSpawnY, 0);
    }

    public void SetSpeed(float set)
    {
        speed = set;
    }
}
