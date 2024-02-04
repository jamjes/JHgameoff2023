using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool RunLevelTimer { private set; get; }

    private void OnEnable()
    {
        PlayerInputController.OnPauseEnter += ToggleTimerState;
    }

    private void OnDisable()
    {
        PlayerInputController.OnPauseEnter -= ToggleTimerState;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        RunLevelTimer = true;
    }

    private void ToggleTimerState()
    {
        RunLevelTimer = !RunLevelTimer;
    }
}
