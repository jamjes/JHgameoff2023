using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelScore : MonoBehaviour
{
    private int score = 0;
    [SerializeField] private TextMeshProUGUI scoreLabel;

    private void OnEnable()
    {
        Coin.OnCoinCollision += IncrementScore;
    }

    private void OnDisable()
    {
        Coin.OnCoinCollision -= IncrementScore;
    }

    void Start()
    {
        
    }

    void IncrementScore()
    {
        score++;
        scoreLabel.text = score.ToString("000");
    }
}
