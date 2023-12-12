using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDetection : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("obstacle"))
        {
            //Debug.Log("Hit");
            GetComponent<Player_Stat>().TakeHit(1);
        }
        else if (other.gameObject.CompareTag("Bomb"))
        {
            //Debug.Log("Hit");
            GetComponent<Player_Stat>().TakeHit(1);
            other.GetComponent<bombaScript>().MacronExplosion();
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
