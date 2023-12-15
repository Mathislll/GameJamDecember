using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombaScript : MonoBehaviour
{
    private float speed;
    public float verticalSpeed = 5f; // Vitesse de déplacement vertical
    public float verticalDirection = 1f;
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
    public float upperLimit = 10f; // Limite supérieure pour le mouvement vertical
    public float lowerLimit = -10f;
    public float rotationAmount = 180f;

    private bool hasRotated = false;

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
            Vector3 horizontalMove = new Vector3(-1, 0, 0) * speed * Time.deltaTime;
            transform.position -= horizontalMove;

            Vector3 verticalMove = new Vector3(0, verticalSpeed * Time.deltaTime * verticalDirection, 0);
            transform.position += verticalMove;

            if ((transform.position.y > upperLimit || transform.position.y < lowerLimit) && !hasRotated)
            {
                verticalDirection *= -1f;
                transform.Rotate(0, 0, rotationAmount);
                hasRotated = true;
            }
            else if (transform.position.y <= upperLimit && transform.position.y >= lowerLimit)
            {
                hasRotated = false;
            }
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
