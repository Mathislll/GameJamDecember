using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pilierScript : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float pillardGap;
    [SerializeField]
    private float pillardPos;

    [SerializeField]
    private GameObject topPillard;
    [SerializeField]
    private GameObject bottomPillard;
    [SerializeField]
    private Rigidbody rb;

    private float[] limitPillardPos = new float[] {4.25f,-4.25f};
    private const float spawnPosition = 30;

    void Start()
    {
        if(pillardPos > limitPillardPos[0] || pillardPos < limitPillardPos[1])
        {
            Debug.LogError("la position du pilier va dépasser l'écran");
            Debug.Break();
            
        }
        if(pillardPos <= 0)
        {
            Debug.LogError("les pilier ne laissent pas d'ouverture");
            Debug.Break();
        }

        speed = speed * -10f;
        topPillard.transform.localPosition += new Vector3(0,pillardGap,0);
        transform.position = new Vector3(spawnPosition,pillardPos,0);
    }

    void Update()
    {
        if(transform.position.x < -20)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        //transform.Translate(speed,0,0);
        rb.velocity = new Vector3(speed * Time.fixedDeltaTime,0,0);
    }
}
