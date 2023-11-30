using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShrinkController : MonoBehaviour
{
    public bool countdown = false;
    public float Timer;
    public float timerRef;
    public PlayerMovementController _playerMovement;
    public delegate void ShrinkEvent();
    public static event ShrinkEvent OnShrinkEnd;

    private void OnEnable()
    {
        PlayerQTEController.OnQTEWin += Shrink;
        PlayerCollisionHandler.OnShrinkExit += UnShrink;
    }

    private void OnDisable()
    {
        PlayerQTEController.OnQTEWin -= Shrink;
        PlayerCollisionHandler.OnShrinkExit -= UnShrink;
    }

    private void Shrink()
    {
        StartCoroutine(ShrinkAfterDelay(.1f));
        _playerMovement.SetPhysicsMode(0);
    }

    private void UnShrink()
    {
        transform.localScale = new Vector3(1, 1, 1);
        _playerMovement.SetPhysicsMode(1);
        OnShrinkEnd();
    }

    private void Update()
    {
        /*if (countdown)
        {
            timerRef -= Time.deltaTime;
            if (timerRef <= 0)
            {
                UnShrink();
                countdown = false;
                timerRef = Timer;
            }
        }*/
    }

    private IEnumerator ShrinkAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        transform.localScale = new Vector3(.3f, .3f, .3f);
        //Calculate Countdown Duration
        Debug.Log("Shrink");
        timerRef = Timer;
        countdown = true;
    }
}
