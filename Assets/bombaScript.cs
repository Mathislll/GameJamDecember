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

            float verticalMove = verticalSpeed * Time.deltaTime * verticalDirection;
            transform.Translate(0, verticalMove, 0);

            if (transform.position.y > upperLimit || transform.position.y < lowerLimit)
            {
                verticalDirection *= -1f;
                //transform.Rotate(0, 0, rotationAmount); // Effectue une rotation de 180 degrés autour de l'axe Z
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
