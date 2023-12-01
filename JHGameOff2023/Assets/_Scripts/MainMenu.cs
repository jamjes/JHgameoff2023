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

    public AudioSource Music;
    public AudioSource SoundFX;


    private void Start()
    {
        Cursor.visible = true;
        MainPanel.SetActive(true);
        MenuPanel.SetActive(false);
        Music.Play();
        Settings.BG_MUSIC = true;
        Settings.SOUND_FX = true;
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Level 1");
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
            SoundFX.Play();
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
            Music.volume = .8f;
        }
        else
        {
            MusicLabel.text = "X";
            Music.volume = 0f;
        }
    }

    public void End()
    {
        Application.Quit();
    }
}
