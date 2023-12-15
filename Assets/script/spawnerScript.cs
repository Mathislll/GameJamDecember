using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class spawnerScript : MonoBehaviour
{
#pragma warning disable CS0414
    [SerializeField]
    private GameObject[] pillard;
    [SerializeField]
    private GameObject boss;

    [SerializeField]
    private float timeBetweenPillard;

    [SerializeField]
    private float timeBeforeBoss;

    [SerializeField]
    public float speedObstacle;

    private float timer;
    private float timerBoss;

    private float randomGap = 1;
    private float randomPosition = 1;
    private int randomPillard;

    private bool bossScene = false;
    private bool bossAlive = false;

    private const float bossPositionX = 5;
    private SoundManager soundManager;

    void Start()
    {
        setRandomNumber();
        if (SoundManager.Instance != null)
        {
            soundManager = SoundManager.Instance;
        }
        else Debug.LogError("SoundManager is null");
    }

    void Update()
    {
        if(!bossScene)
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
            pil.GetComponent<pilierScript>().SetSpeed(speedObstacle);
        }
        else if(pil.GetComponent<bombaScript>() != null )
        {
            //pil.transform.position = new Vector3(20,pil.transform.position.y,0);
            pil.GetComponent<bombaScript>().positionSpawnY = 0;
            pil.GetComponent<bombaScript>().SetIsMove();
            pil.GetComponent<bombaScript>().SetSpeed(speedObstacle);
        }
        pil.transform.parent = null;
    }

    private void setRandomNumber()
    {
        randomPillard = UnityEngine.Random.Range(0, pillard.Length);
        //Debug.Log(randomPillard.ToString());
        randomGap = UnityEngine.Random.Range(2.1f, 4f);
        randomPosition = UnityEngine.Random.Range(-2.00f, 2.00f);
        //Debug.Log(randomGap.ToString() + "," + randomPosition.ToString());
    }

    private void SpawnBoss()
    {
        bossAlive = true;
        GameObject elBoss = Instantiate(boss, this.transform);
        elBoss.transform.parent = null;
        elBoss.transform.position = new Vector3(30, 0, 0);
        StartCoroutine(BossCome(elBoss));
        soundManager.PlayBossMusic();
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
        return timeBeforeBoss;
    }

    public float GetTimeLevel()
    {
        return timerBoss;
    }

    IEnumerator BossCome(GameObject boss)
    {
        while(boss.transform.position.x > bossPositionX)
        {
            boss.GetComponent<Rigidbody>().velocity = new Vector3(-speedObstacle * 50f * Time.fixedDeltaTime, 0, 0);
            //Debug.Log(boss.transform.position.x.ToString()+";"+bossPositionX.ToString());
            yield return new WaitForFixedUpdate();
        }
        boss.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        boss.GetComponent<BossShootScript>().SetCanOpenFire(true);
    }
}
