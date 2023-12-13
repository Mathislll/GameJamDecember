using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Stat : MonoBehaviour
{
    public float health = 10f;
    public GameObject turret1;
    public GameObject turret2;
    public GameObject eyeBody;

    public bool isTurret1Dead = false;
    public bool isTurret2Dead = false;
    public bool iseyeBodyDead = false;

    private Animator animator;

    private const float DEAD_POS = -10f;
    private const float SPEED_DEAD = 5f;

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
            this.GetComponent<BossShootScript>().SetCanOpenFire(false);
            StartCoroutine(DeadAnimation());
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
