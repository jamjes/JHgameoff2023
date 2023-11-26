using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void OnEnable()
    {
        PlayerCollisionHandler.OnQTEEnter += Run;
    }

    private void OnDisable()
    {
        PlayerCollisionHandler.OnQTEEnter -= Run;
    }

    private void Run()
    {
        GenerateButtons();
        run = true;
        pointer = 0;
        timerRef = Timer;
        
    }

    private void GenerateButtons()
    {
        for (int pointer = 0; pointer < 3; pointer++)
        {
            int ran = Random.Range(0, 1);

            switch(ran)
            {
                case 0:
                    Buttons[pointer] = "Button1";
                    break;

                case 1:
                    Buttons[pointer] = "Button2";
                    break;

                case 2:
                    Buttons[pointer] = "Button3";
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
                pointer++;

                if (pointer == 3)
                {
                    run = false;
                    timerRef = Timer;
                    OnQTEWin();
                }
            }

            if (timerRef <= 0)
            {
                run = false;
                timerRef = Timer;
                OnQTELoss();
            }
        }
    }
}
