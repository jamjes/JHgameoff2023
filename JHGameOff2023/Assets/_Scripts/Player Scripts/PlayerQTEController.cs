using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerQTEController : MonoBehaviour
{
    public string[] Buttons = new string[3];
    public bool run = false;
    public int pointer;
    public bool win = false;

    public delegate void QTE();
    public static event QTE OnQTEWin;
    public static event QTE OnQTELoss;

    public float Timer;
    private float timerRef;

    public Canvas PlayerHUD;
    public TextMeshProUGUI[] keyLabels;
    public Image[] keyBGs;

    public AudioSource Sound;

    private void OnEnable()
    {
        PlayerCollisionHandler.OnQTEEnter += Run;
        Player.MuteSoundFX += Mute;
        Player.UnmuteSoundFX += Unmute;
    }

    private void OnDisable()
    {
        PlayerCollisionHandler.OnQTEEnter -= Run;
        Player.MuteSoundFX -= Mute;
        Player.UnmuteSoundFX -= Unmute;
    }

    private void Mute()
    {
        Sound.volume = 0;
    }

    private void Unmute()
    {
        Sound.volume = 0.8f;
    }

    private void Start()
    {
        if (PlayerHUD.gameObject.activeSelf)
        {
            PlayerHUD.gameObject.SetActive(false);
        }
    }

    private void Run()
    {
        GenerateButtons();
        run = true;
        pointer = 0;
        timerRef = Timer;
        PlayerHUD.gameObject.SetActive(true);
        foreach(Image i in keyBGs)
        {
            i.color = Color.white;
        }

    }

    private void GenerateButtons()
    {
        for (int pointer = 0; pointer < 3; pointer++)
        {
            int ran = Random.Range(0, 3);

            switch(ran)
            {
                case 0:
                    Buttons[pointer] = "Button1";
                    keyLabels[pointer].text = "Q";
                    break;

                case 1:
                    Buttons[pointer] = "Button2";
                    keyLabels[pointer].text = "W";
                    break;

                case 2:
                    Buttons[pointer] = "Button3";
                    keyLabels[pointer].text = "E";
                    break;
            }
        }
    }

    private void Update()
    {
        if (run)
        {
            timerRef -= Time.deltaTime;

            if (Input.GetButtonDown(Buttons[pointer]))
            {
                Buttons[pointer] = "CORRECT";
                keyBGs[pointer].color = Color.green;
                pointer++;

                if (pointer == 3)
                {
                    run = false;
                    timerRef = Timer;
                    if (OnQTEWin != null) OnQTEWin();
                    PlayerHUD.gameObject.SetActive(false);
                }
            }

            if (timerRef <= 0)
            {
                run = false;
                timerRef = Timer;
                if (OnQTELoss != null) OnQTELoss();
                PlayerHUD.gameObject.SetActive(false);
                Sound.Play();
            }
        }
    }
}
