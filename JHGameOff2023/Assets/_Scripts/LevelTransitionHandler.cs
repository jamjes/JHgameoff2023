using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransitionHandler : MonoBehaviour
{
    public KeyCode trigger;
    public string targetScene;
    
    void Update()
    {
        if (Input.GetKeyDown(trigger))
        {
            SceneManager.LoadScene(targetScene);
        }
    }
}
