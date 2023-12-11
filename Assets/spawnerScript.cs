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

    private float randomGap = 1;
    private float randomPosition = 1;

    void Start()
    {
        setRandomNumber();
        spawnPillard();
    }

    void Update()
    {
        if(timer > timeBetweenPillard)
        {
            timer = 0;
            spawnPillard();
            setRandomNumber();
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    void spawnPillard()
    {
        GameObject pil = Instantiate(pillard);
        pil.GetComponent<pilierScript>().setPillard(randomGap, randomPosition);
        pil.transform.parent = null;
    }

    private void setRandomNumber()
    {
        randomGap = UnityEngine.Random.Range(1f, 6f);
        randomPosition = UnityEngine.Random.Range(-4.20f, 4.20f);
        Debug.Log(randomGap.ToString() + "," + randomPosition.ToString());
    }
}
