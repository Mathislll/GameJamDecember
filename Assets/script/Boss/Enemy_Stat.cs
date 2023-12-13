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

    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        if ((isTurret1Dead == true) && (isTurret2Dead == true) && (iseyeBodyDead == true))
        {
            GameManager.Instance.isFinalBossDefeated = true;
            PlayDeathAnimation();
        }
    }

    public void PlayDeathAnimation()
    {
        animator.SetTrigger("Death");
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
}
