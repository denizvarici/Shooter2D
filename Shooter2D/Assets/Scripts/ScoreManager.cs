using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private float score;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Update()
    {
        score += (1 * Time.deltaTime);
        GameSystemManager.Instance.score = Convert.ToInt32(score);
        PlayerPrefs.SetInt("score", Convert.ToInt32(score));
        scoreText.text = GameSystemManager.Instance.score.ToString();
        if (GameSystemManager.Instance.score >= GameSystemManager.Instance.highScore)
        {
            GameSystemManager.Instance.highScore = GameSystemManager.Instance.score;
            PlayerPrefs.SetInt("highScore", GameSystemManager.Instance.highScore);
        }
    }
}
