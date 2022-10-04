using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;

public class StartSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;

    [SerializeField] private GameObject optionsGun;
    [SerializeField] private GameObject lightObject;
    private new Light2D light;
    private bool playGame = false;

    private void Start()
    {
        light = lightObject.GetComponent<Light2D>();
        light.intensity = 0;
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        optionsGun.SetActive(false);
    }

    private void Update()
    {
        if (playGame)
        {
            StartCoroutine(LightCoroutine());
        }
    }

    public void EnterOptionsButton()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
        optionsGun.SetActive(true);
    }
    public void ExitOptionsButton()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        optionsGun.SetActive(false);
    }

    public void PlayButton()
    {
        //SceneManager.LoadScene("GameScene");
        playGame = true;

    }

    public void ExitButton()
    {
        Application.Quit();
    }

    IEnumerator LightCoroutine()
    {
        yield return new WaitForSeconds(1f);
        if (light.intensity <= 40f)
        {
            light.intensity += 0.5f;
        }
        else
        {
            SceneManager.LoadScene("GameScene");
        }       
    }

}
