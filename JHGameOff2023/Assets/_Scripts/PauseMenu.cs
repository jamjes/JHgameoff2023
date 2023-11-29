using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public delegate void Pause();
    public static event Pause OnPauseExit;

    public void Resume()
    {
        if (OnPauseExit != null)
        {
            Debug.Log("Unpause");
            OnPauseExit();
        }
    }
}
