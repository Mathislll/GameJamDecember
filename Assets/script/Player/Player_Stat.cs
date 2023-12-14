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

    private float blinkInterval = 0.2f; // Durée entre les changements de couleur
    private float nextBlinkTime = 0f; // Prochain temps pour changer la couleur
    private bool isBlinking = false;

    public float delayBeforeRespawn = 3f;

    private GameManager gameManager;
    private SoundManager soundManager;
    private AudioSource audioSFXSource;

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

        if (SoundManager.Instance != null)
        {
            soundManager = SoundManager.Instance;
            audioSFXSource = soundManager.sfxSource;
        }
        else Debug.LogError("SoundManager is null");
    }

    public void GodMod()
    {
        isGodMode = !isGodMode;
    }

    void Update()
    {
        if (isRespawning)
        {
            Blink();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            GodMod();
        }
    }

    public void Blink()
    {
        if (Time.time >= nextBlinkTime)
        {
            // Change la couleur du matériau
            playerMaterial.SetColor("_BaseColor", isBlinking ? normalColor : blinkColor);
            isBlinking = !isBlinking;

            // Définit le temps pour le prochain changement de couleur
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
        if (soundManager != null)
        {
            audioSFXSource.clip = soundManager.characterDie;
            audioSFXSource.Play();
        }
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

        // Désactivez le Collider et la physique
        if (rb != null) rb.isKinematic = true;
        if (sphereCollider != null) sphereCollider.enabled = false;

        float endTime = Time.time + delayBeforeRespawn; // Définit le temps de fin

        // Boucle jusqu'à ce que le temps soit écoulé ou que le joueur appuie sur "Jump"
        while (Time.time < endTime)
        {
            if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire"))
            {
                break; // Sort de la boucle si "Jump" est pressé
            }
            yield return null; // Attend jusqu'à la prochaine frame
        }

        // Réactivez le Collider et la physique
        if (rb != null) rb.isKinematic = false;
        if (sphereCollider != null) sphereCollider.enabled = true;
        isRespawning = false;
        playerMaterial.SetColor("_BaseColor", normalColor); // Réinitialise la couleur du matériau
    }

}
