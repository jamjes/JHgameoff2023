using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject MainPanel;
    public GameObject MenuPanel;
    public GameSettings Settings;

    public TextMeshProUGUI MusicLabel;
    public TextMeshProUGUI SoundLabel;


    private void Start()
    {
        MainPanel.SetActive(true);
        MenuPanel.SetActive(false);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("TechDemo");
    }

    public void OpenOptionsMenu()
    {
        MainPanel.SetActive(false);
        MenuPanel.SetActive(true);
    }

    public void CloseOptionsMenu()
    {
        MainPanel.SetActive(true);
        MenuPanel.SetActive(false);
    }

    public void ToggleSoundFX()
    {
        Settings.SOUND_FX = !Settings.SOUND_FX;
        if (Settings.SOUND_FX)
        {
            SoundLabel.text = "O";
        }
        else
        {
            SoundLabel.text = "X";
        }
    }

    public void ToggleBGMusic()
    {
        Settings.BG_MUSIC = !Settings.BG_MUSIC;
        if (Settings.BG_MUSIC)
        {
            MusicLabel.text = "O";
        }
        else
        {
            MusicLabel.text = "X";
        }
    }

    public void End()
    {
        Application.Quit();
    }
}
