using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSystem : MonoBehaviour
{
    public GameObject player;
    public GameObject lifeContainer;
    public GameObject lifeImagePrefab;
    public float spacing = 50f;

    void Start()
    {
        UpdateLivesDisplay();
    }

    public void UpdateLivesDisplay() // UpdateLivesDisplay est appelé dans un event du MenuScript
    {
        float playerLife = player.GetComponent<Player_Stat>().life; // récup les vies du joueurs
        
        foreach (Transform child in lifeContainer.transform) // Supprimer les anciennes images de vie
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < playerLife; i++) // Instancier une nouvelle image de vie pour chaque vie restante
        {
            GameObject lifeImage = Instantiate(lifeImagePrefab, lifeContainer.transform);
            lifeImage.transform.localPosition = new Vector3(i * spacing, 0, 0);
        }
    }

    void Update() 
    {
        // UpdateLivesDisplay n'est appelé que quand il perd des pv il est plus appelé a chaque frame
    }
}
