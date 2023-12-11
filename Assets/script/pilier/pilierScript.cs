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

    private float[] limitPillardPos = new float[] {4.25f,-4.25f};

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

        topPillard.transform.localPosition += new Vector3(0,pillardGap,0);
        transform.position = new Vector3(0,pillardPos,0);
    }

    void Update()
    {
        
    }
}
