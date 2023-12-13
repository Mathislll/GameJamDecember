using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreSystem : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance != null)
        {
            gameManager = GameManager.Instance;
        }
        else
        {
            Debug.Log("GameManager is null");
        }

        UpdateScore();
    }

    public void UpdateScore()
    {
        scoreText.text = "Score: " + gameManager.actualScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
