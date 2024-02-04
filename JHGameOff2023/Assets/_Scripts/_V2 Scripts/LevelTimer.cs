using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class LevelTimer : MonoBehaviour
{
    private float elapsedTime = 0;
    [SerializeField] private TextMeshProUGUI timeLabel;
    
    void Update()
    {
        if (GameManager.instance.RunLevelTimer)
        {
            elapsedTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);
            timeLabel.text = String.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
