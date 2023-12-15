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
    public Color blinkColor = new Color(0.0f, 0.44f, 1.0f, 1.0f); // Couleur de clignotement
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

    public GameObject FBXtoBlink;
    public GameObject FBXtoBlink2;


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
            // Convertissez la couleur hexadécimale #006CFF en un objet Color de Unity
            Color blinkEmissionColor = new Color(0.0f, 0.44f, 1.0f, 1.0f); // Représentation RGB de #006CFF

            // Obtenez le Renderer et le Matériau du GameObject FBX
            Renderer fbxRenderer = FBXtoBlink.GetComponent<Renderer>();
            Material fbxMaterial = fbxRenderer.material;

            Renderer fbxRenderer2 = FBXtoBlink2.GetComponent<Renderer>();
            Material fbxMaterial2 = fbxRenderer2.material;

            // Activez ou désactivez l'émission du matériau
            if (isBlinking)
            {
                fbxMaterial.DisableKeyword("_EMISSION");
                fbxMaterial2.DisableKeyword("_EMISSION");
            }
            else
            {
                fbxMaterial.EnableKeyword("_EMISSION");
                fbxMaterial.SetColor("_EmissionColor", blinkEmissionColor); // Utilisez la couleur spécifique pour l'émission
                fbxMaterial2.EnableKeyword("_EMISSION");
                fbxMaterial2.SetColor("_EmissionColor", blinkEmissionColor); // Utilisez la couleur spécifique pour l'émission
            }
            isBlinking = !isBlinking;

            // Définit le temps pour le prochain changement
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
            if (soundManager != null)
            {
                audioSFXSource.clip = soundManager.characterDie;
                audioSFXSource.Play();
            }
            LittleGodModeRespawn();
            StartCoroutine(RespawnCoroutine());
        }
    }

    IEnumerator LittleGodModeRespawn()
    {
        isGodMode = true;
        yield return new WaitForSeconds(1f);
        isGodMode = false;
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

        Renderer fbxRenderer = FBXtoBlink.GetComponent<Renderer>();
        Material fbxMaterial = fbxRenderer.material;
        fbxMaterial.DisableKeyword("_EMISSION");

        Renderer fbxRenderer2 = FBXtoBlink2.GetComponent<Renderer>();
        Material fbxMaterial2 = fbxRenderer2.material;
        fbxMaterial2.DisableKeyword("_EMISSION");

        // Réactivez le Collider et la physique
        if (rb != null) rb.isKinematic = false;
        if (sphereCollider != null) sphereCollider.enabled = true;
        isRespawning = false;
        playerMaterial.SetColor("_BaseColor", normalColor); // Réinitialise la couleur du matériau
    }

}
