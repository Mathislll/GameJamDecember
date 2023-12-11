using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class spawnerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject pillard;

    [SerializeField]
    private float timeBetweenPillard;

    private float timer;

    void Start()
    {
        spawnPillard();
    }

    void Update()
    {
        
    }

    void spawnPillard()
    {
        GameObject pil = Instantiate(pillard);
        pil.transform.parent = null;
    }
}
