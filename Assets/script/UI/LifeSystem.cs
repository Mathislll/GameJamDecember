using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSystem : MonoBehaviour
{
    public GameObject player;
    public GameObject lifeContainer;
    public float spacing = 50f;
    public GameObject lifeImagePrefab;

    // Start is called before the first frame update
    void Start()
    {
        UpdateLivesDisplay();
    }

    void UpdateLivesDisplay()
    {
        float playerLife = player.GetComponent<Player_Stat>().life;

        // Supprimer les anciennes images de vie
        foreach (Transform child in lifeContainer.transform)
        {
            Destroy(child.gameObject);
        }

        // Instancier une nouvelle image de vie pour chaque vie restante
        for (int i = 0; i < playerLife; i++)
        {
            GameObject lifeImage = Instantiate(lifeImagePrefab, lifeContainer.transform);
            lifeImage.transform.localPosition = new Vector3(i * spacing, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLivesDisplay();
    }
}
