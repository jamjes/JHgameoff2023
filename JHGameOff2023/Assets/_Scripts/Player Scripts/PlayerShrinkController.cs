using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShrinkController : MonoBehaviour
{
    public bool countdown = false;
    public float timer;
    public float timerRef;
    private void OnEnable()
    {
        PlayerQTEController.OnQTEWin += Shrink;
    }

    private void OnDisable()
    {
        PlayerQTEController.OnQTEWin -= Shrink;
    }

    private void Shrink()
    {
        StartCoroutine(ShrinkAfterDelay(.2f));
    }

    private void UnShrink()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }

    private void Update()
    {
        if (countdown)
        {
            timerRef -= Time.deltaTime;
            if (timerRef <= 0)
            {
                UnShrink();
                countdown = false;
                timerRef = timer;
            }
        }
    }

    private IEnumerator ShrinkAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        transform.localScale = new Vector3(.5f, .5f, .5f);
        //Calculate Countdown Duration
        timerRef = timer;
        countdown = true;
    }
}
