using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombaScript : MonoBehaviour
{
    [SerializeField]
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
        speed = speed * -10f;
    }

    void Update()
    {
        if (transform.position.x < -20)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (trapMove)
        {
            rb.velocity = new Vector3(speed * Time.fixedDeltaTime, 0, 0);
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
        Destroy(this.gameObject);
    }

    public void SetIsMove()
    {
        trapMove = true;
        this.transform.position = new Vector3(spawnPositionX, positionSpawnY, 0);
    }
}
