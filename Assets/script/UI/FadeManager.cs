using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    public bool visible = true;
    private GameManager gameManager;
    public string levelToLoad;

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
     if (GameManager.Instance != null)
        {
            gameManager = GameManager.Instance;
        }

     animator = GetComponent<Animator>();
        Debug.Log("FadeManager Start");
        animator.SetTrigger("FadeOut");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetVisible()
    {
        visible = !visible;
        gameObject.SetActive(visible); 
    }

    public void CallNextLevel()
    {
        gameManager.LoadNextLevel(levelToLoad);
    }
}
