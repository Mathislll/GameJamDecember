using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchLevelMusic : MonoBehaviour
{

    private SoundManager soundManager;
    // Start is called before the first frame update
    void Start()
    {
        if (soundManager == null)
        {
            soundManager = SoundManager.Instance;
            soundManager.PlayLevelMusic();
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
