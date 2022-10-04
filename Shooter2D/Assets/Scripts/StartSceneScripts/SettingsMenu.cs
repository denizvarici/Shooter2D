using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioMixer musicMixer;

    public Slider volumeSlider;
    public Slider musicSlider;

    public TMPro.TMP_Dropdown graphicsDropdown;
    private int graphicsIndex;

    public TMPro.TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;

    public void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("volume");
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");

        graphicsIndex = PlayerPrefs.GetInt("graphicsQuality");
        graphicsDropdown.value = graphicsIndex;
        SetGraphics(graphicsIndex);

        if (PlayerPrefs.GetInt("isFullscreen") == 1)
        {
            SetFullScreen(true);
        }
        if (PlayerPrefs.GetInt("isFullscreen") == 0)
        {
            SetFullScreen(false);
        }
        

        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
        
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume(float volume)
    {
        var dbVolume = Mathf.Log10(volume) * 30;
        if (volume == 0.0f)
        {
            dbVolume = -80.0f;
        }
        audioMixer.SetFloat("volume", dbVolume);
        PlayerPrefs.SetFloat("volume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        var dbVolume = Mathf.Log10(volume) * 20;
        if (volume == 0.0f)
        {
            dbVolume = -80.0f;
        }
        musicMixer.SetFloat("musicVolume", dbVolume);
        PlayerPrefs.SetFloat("musicVolume", volume);

    }

    public void SetGraphics(int graphicIndex)
    {
        QualitySettings.SetQualityLevel(graphicIndex);
        PlayerPrefs.SetInt("graphicsQuality", graphicIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        PlayerPrefs.SetInt("isFullscreen", Convert.ToInt32(isFullScreen));
    }
}
