using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    public TextMeshProUGUI text;

    void Start()
    {
        StartCoroutine(DelayedBegin());
    }

    IEnumerator DelayedBegin()
    {
        yield return new WaitForSeconds(1);
        text.text = "2";
        yield return new WaitForSeconds(1);
        text.text = "1";
        yield return new WaitForSeconds(1);
        text.text = "Go!";
        yield return new WaitForSeconds(.3f);
        Destroy(this.gameObject);
    }
}
