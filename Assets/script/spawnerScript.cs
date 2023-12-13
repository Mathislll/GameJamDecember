using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class spawnerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject[] pillard;
    [SerializeField]
    private GameObject boss;

    [SerializeField]
    private float timeBetweenPillard;

    [SerializeField]
    private float timeBeforeBoss;

    private float timer;
    private float timerBoss;

    private float randomGap = 1;
    private float randomPosition = 1;
    private int randomPillard;

    private bool bossScene = false;

    void Start()
    {
        setRandomNumber();
        spawnPillard();
    }

    void Update()
    {
        if(timer < timeBeforeBoss)
        {
            this.LvlState();
            //Debug.Log(timerBoss);
        }
    }

    void spawnPillard()
    {
        GameObject pil = Instantiate(pillard[randomPillard]);
        if (pil.GetComponent<pilierScript>() != null )
        {
            pil.GetComponent<pilierScript>().setPillard(randomGap, randomPosition);
        }
        else if(pil.GetComponent<bombaScript>() != null )
        {
            //pil.transform.position = new Vector3(20,pil.transform.position.y,0);
            pil.GetComponent<bombaScript>().positionSpawnY = randomPosition;
            pil.GetComponent<bombaScript>().SetIsMove();
        }
        pil.transform.parent = null;
    }

    private void setRandomNumber()
    {
        randomPillard = UnityEngine.Random.Range(0, pillard.Length);
        //Debug.Log(randomPillard.ToString());
        randomGap = UnityEngine.Random.Range(1f, 6f);
        randomPosition = UnityEngine.Random.Range(-4.00f, 4.00f);
        //Debug.Log(randomGap.ToString() + "," + randomPosition.ToString());
    }

    private void SpawnBoss()
    {
        GameObject elBoss = Instantiate(boss, this.transform);
        elBoss.transform.position = new Vector3(20, 0, 0);
        elBoss.transform.parent = null;
        //StartCoroutine(BossCome(elBoss));
    }

    void LvlState()
    {
        if (timer > timeBetweenPillard && !bossScene)
        {
            timer = 0;
            spawnPillard();
            setRandomNumber();
        }
        else
        {
            timer += Time.deltaTime;
        }

        if (timerBoss > timeBeforeBoss)
        {
            SpawnBoss();
            bossScene = true;
        }
        else
        {
            timerBoss += Time.deltaTime;
        }
    }

    public float GetTimeBoss()
    {
        return timerBoss;
    }

    IEnumerator BossCome(GameObject boss)
    {
        while (true)
        {
            boss.transform.position = new Vector3(boss.transform.position.x - 1, 0, 0);
            yield return null;
        }
    }
}
