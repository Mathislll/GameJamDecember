using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombaScript : MonoBehaviour
{
    [SerializeField]
    private GameObject bubulParticul;
    [SerializeField]
    private GameObject boomParticul;
    [SerializeField]
    private GameObject obstacleToDestroy;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void MacronExplosion()
    {
        GameObject bubul = Instantiate(bubulParticul,this.transform);
        GameObject boom = Instantiate(boomParticul, this.transform);
        bubul.transform.parent = null;
        boom.transform.parent = null;
        Destroy(obstacleToDestroy.gameObject);
        Destroy(this.gameObject);
    }
}
