using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player_Stat : MonoBehaviour
{
    public float life = 3f;
    public Transform respawnPoint;
    public Rigidbody rb;
    public SphereCollider sphereCollider;

    private Renderer playerRenderer;
    public Color blinkColor = Color.white; // Couleur de clignotement
    public Color normalColor = Color.green; // Couleur normale
    private Material playerMaterial;
    public bool isRespawning = false;

    private float blinkInterval = 0.2f; // Dur�e entre les changements de couleur
    private float nextBlinkTime = 0f; // Prochain temps pour changer la couleur
    private bool isBlinking = false;

    public float delayBeforeRespawn = 3f;

    private GameManager gameManager;

    public MenuScript UI; // UI du joueur

    public bool isGodMode = false;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();

        playerRenderer = GetComponent<Renderer>();
        if (playerRenderer != null)
        {
            playerMaterial = playerRenderer.material;
        }

        if (GameManager.Instance != null)
        {
            gameManager = GameManager.Instance;
        }
        else Debug.LogError("GameManager is null");
    }

    void Update()
    {
        if (isRespawning)
        {
            Blink();
        }
    }

    public void Blink()
    {
        if (Time.time >= nextBlinkTime)
        {
            // Change la couleur du mat�riau
            playerMaterial.SetColor("_BaseColor", isBlinking ? normalColor : blinkColor);
            isBlinking = !isBlinking;

            // D�finit le temps pour le prochain changement de couleur
            nextBlinkTime = Time.time + blinkInterval;
        }
    }

    public void TakeHit(float damage)
    {
        if (isGodMode) return;
        life -= damage;
        UI.updateLife.Invoke();
        if (life <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(RespawnCoroutine());
        }
    }

    public void Die()
    {
        UI.playerDead.Invoke();
        Destroy(gameObject);
    }

    public void TakeHeal(float heal)
    {
        life += heal;
    }


    IEnumerator RespawnCoroutine()
    {
        transform.position = respawnPoint.position;
        isRespawning = true;

        // D�sactivez le Collider et la physique
        if (rb != null) rb.isKinematic = true;
        if (sphereCollider != null) sphereCollider.enabled = false;

        float endTime = Time.time + delayBeforeRespawn; // D�finit le temps de fin

        // Boucle jusqu'� ce que le temps soit �coul� ou que le joueur appuie sur "Jump"
        while (Time.time < endTime)
        {
            if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire"))
            {
                break; // Sort de la boucle si "Jump" est press�
            }
            yield return null; // Attend jusqu'� la prochaine frame
        }

        // R�activez le Collider et la physique
        if (rb != null) rb.isKinematic = false;
        if (sphereCollider != null) sphereCollider.enabled = true;
        isRespawning = false;
        playerMaterial.SetColor("_BaseColor", normalColor); // R�initialise la couleur du mat�riau
    }

}
