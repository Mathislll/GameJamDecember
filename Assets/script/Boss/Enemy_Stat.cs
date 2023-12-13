using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Stat : MonoBehaviour
{
    public float health = 10f;
    public GameObject turret1;
    public GameObject turret2;
    public GameObject eyeBody;

    public GameObject explosion;

    public bool isTurret1Dead = false;
    public bool isTurret2Dead = false;
    public bool iseyeBodyDead = false;

    private Animator animator;

    private const float DEAD_POS = -10f;
    private const float SPEED_DEAD = 5f;

    private bool isDying = false;

    void Start()
    {
        if (GetComponent<Animator>() != null)
        {
            animator = GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("Animator is null");
        }
    }

    void Update()
    {
        if ((isTurret1Dead == true) && (isTurret2Dead == true) && (iseyeBodyDead == true))
        {
            GameManager.Instance.isFinalBossDefeated = true;
        }
        if(GameManager.Instance.isFinalBossDefeated && !isDying)
        {
            isDying = true;
            this.GetComponent<BossShootScript>().SetCanOpenFire(false);
            StartCoroutine(DeadAnimation());
            StartCoroutine(CaPetePartout());
        }
    }

    public void TakeHit(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            iseyeBodyDead = true;
            Destroy(eyeBody);
        }
    }
    IEnumerator CaPetePartout()
    {
        while (true)
        {
            GameObject explode = Instantiate(explosion, this.transform);
            explode.transform.localPosition = new Vector3(0, UnityEngine.Random.Range(-2.5f, 2.5f), UnityEngine.Random.Range(0, -10));
            explode.transform.parent = null;
            yield return new WaitForSeconds(0.5f);
        }
    }
    IEnumerator DeadAnimation()
    {
        while (this.transform.position.y > DEAD_POS)
        {
            this.GetComponent<Rigidbody>().velocity = new Vector3(0, -SPEED_DEAD * 10 * Time.fixedDeltaTime, 0);
            yield return new WaitForFixedUpdate();
        }
        Destroy(gameObject);
    }
}
