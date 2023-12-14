using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateHelice : MonoBehaviour
{

    public float speed = 3f; // Vitesse de rotation de l'hélice
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // rotation de l'hélice sur l'axe Y
        transform.Rotate(0, speed * Time.deltaTime,0 );
    }
}
