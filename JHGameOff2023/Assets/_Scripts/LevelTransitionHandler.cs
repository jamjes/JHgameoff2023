using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransitionHandler : MonoBehaviour
{
    public KeyCode trigger;
    public string targetScene;
    public GameObject OptionsPanel, MainPanel;

    private void OnEnable()
    {
        KillzoneEventManager.OnDeathEnter += Restart;
    }

    private void OnDisable()
    {
        KillzoneEventManager.OnDeathEnter -= Restart;
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

    public void LoadMainMenu()
    {
        Debug.Log("Go To Main Menu");
    }

    public void LoadMainLevel()
    {
        Debug.Log("Load Game from Beginning");
    }

    public void LoadCredits()
    {
        Debug.Log("Go To Credit");
    }

    public void ToggleMusic()
    {
        Debug.Log("Toggle Music");
    }

    public void ToggleSoundFX()
    {
        Debug.Log("Toggle Sound Effects");
    }

    public void OpenOptionsMenu()
    {
        Debug.Log("Open Options Menu");
        MainPanel.SetActive(false);
        OptionsPanel.SetActive(true);
    }

    public void CloseOptionsMenu()
    {
        Debug.Log("Close Options Menu");
        MainPanel.SetActive(true);
        OptionsPanel.SetActive(false);
    }

    public void EndGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void PauseGame()
    {
        Debug.Log("Game is Paused");
    }

    public void ResumeGame()
    {
        Debug.Log("Resuming Game");
    }
}
