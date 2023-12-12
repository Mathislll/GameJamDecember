using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody myRigidbody;
    public float flapStrenght;
    private SoundManager soundManager;

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
            myRigidbody.velocity = Vector3.up * flapStrenght;
            soundManager.PlayJumpingSound();
        }

    }
}
