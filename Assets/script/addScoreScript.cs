using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addScoreScript : MonoBehaviour
{
    public int scoreToAdd = 1;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(scoreToAdd);
                GameManager.Instance.CallUpdateScore();
            }
            else Debug.LogError("GameManager is null");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
