using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class EndGameSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject scoreLight;
    private new Light2D light;


    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
   

    private void Start()
    {
        light = scoreLight.GetComponent<Light2D>();
        light.intensity = 0;
        light.pointLightInnerRadius = 0f;
        scoreText.text = "Score\n" + PlayerPrefs.GetInt("score");
        highScoreText.text = "Hýgh Score\n" + PlayerPrefs.GetInt("highScore");
    }

    private void Update()
    {
        StartCoroutine(IncreaseLight());
        
    }

    IEnumerator IncreaseLight()
    {
        yield return new WaitForSeconds(1f);
        if (light.pointLightInnerRadius <= 5f)
        {
            light.pointLightInnerRadius += 0.01f;
        }
        if (light.intensity <= 1.7f)
        {
            light.intensity += 0.01f;
        }

    }

    public void RetryButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("StartScene");
    }
}
