using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float rotationSpeed = 10;
    public float flapStrenght;
    private SoundManager soundManager;

    public ParticleSystem particleSystem1;
    public ParticleSystem particleSystem2;
    public GameObject fbxToRotate; // fait rotate le fbx plutot que tout le corps
    // Start is called before the first frame update
    void Start()
    {
        if (SoundManager.Instance != null)
        {
            soundManager = SoundManager.Instance;
        }
        else Debug.LogError("SoundManager is null");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") == true)
        {
            rb.velocity = Vector3.up * flapStrenght;
            soundManager.PlayJumpingSound();
            particleSystem1.Play();
            particleSystem2.Play();
        }

    }

    void FixedUpdate()
    {
        fbxToRotate.transform.rotation = Quaternion.Euler(-rb.velocity.y * rotationSpeed -90, 90,180);
    }
}
