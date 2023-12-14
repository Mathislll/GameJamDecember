using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pilierScript : MonoBehaviour
{
    public float speed;
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
        if(pillardPos < 0)
        {
            //Debug.LogError("les pilier ne laissent pas d'ouverture");
            //Debug.Break();
        }

        speed = speed * -1f;
        topPillard.transform.localPosition += new Vector3(0,pillardGap,0);
        transform.position = new Vector3(spawnPosition,pillardPos,0);
    }

    void Update()
    {
        if(transform.position.x < -20)
        {
            Destroy(gameObject);
        }
        // changer le X du pilier
        transform.Translate(speed * Time.deltaTime,0,0);

    }

    private void FixedUpdate()
    {
        //transform.Translate(speed,0,0);
    }

    public void setPillard(float gap, float pos)
    {
        this.pillardGap = gap;
        this.pillardPos = pos;
    }

    public void SetSpeed(float set)
    {
        speed = set;
    }
}
