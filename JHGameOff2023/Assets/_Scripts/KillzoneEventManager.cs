using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillzoneEventManager : MonoBehaviour
{
    public delegate void Death();
    public static event Death OnDeathEnter;

    public AudioSource Sound;

    private void OnEnable()
    {
        Player.MuteSoundFX += Mute;
        Player.UnmuteSoundFX += Unmute;
    }

    private void OnDisable()
    {
        Player.MuteSoundFX -= Mute;
        Player.UnmuteSoundFX -= Unmute;
    }

    private void Mute()
    {
        Sound.volume = 0;
    }

    private void Unmute()
    {
        Sound.volume = 0.4f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (OnDeathEnter != null)
            {
                Sound.Play();
                OnDeathEnter();
            }
        }
    }
}
