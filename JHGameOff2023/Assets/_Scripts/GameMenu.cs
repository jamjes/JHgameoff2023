using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public GameSettings Settings;

    private void OnEnable()
    {
        Player.OnPauseEnter += LoadPauseScene;
        PauseMenu.OnPauseExit += UnloadPauseScene;
    }

    private void OnDisable()
    {
        Player.OnPauseEnter -= LoadPauseScene;
        PauseMenu.OnPauseExit -= UnloadPauseScene;
    }

    public void LoadPauseScene()
    {
        if (!Settings.PAUSED)
        {
            Debug.Log("Paused");
            SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
            Settings.PAUSED = true;
        }
    }

    public void UnloadPauseScene()
    {
        SceneManager.UnloadSceneAsync("PauseMenu");
    }
}
