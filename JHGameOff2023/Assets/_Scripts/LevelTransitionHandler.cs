using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransitionHandler : MonoBehaviour
{
    public KeyCode trigger;
    public string targetScene;

    private void OnEnable()
    {
        EventManager.OnDeathEnter += Restart;
    }

    private void OnDisable()
    {
        EventManager.OnDeathEnter -= Restart;
    }

    void Update()
    {
        if (Input.GetKeyDown(trigger))
        {
            SceneManager.LoadScene(targetScene);
        }
    }

    IEnumerator DelayRestart()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    #region Event Functions
    private void Restart()
    {
        StartCoroutine(DelayRestart());
    }

    #endregion
}
