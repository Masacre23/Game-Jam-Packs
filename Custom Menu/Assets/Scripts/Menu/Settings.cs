﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [HideInInspector]
    public float soundsVolume = 1.0f;
    [HideInInspector]
    public float musicVolume = 1.0f;
    [HideInInspector]
    public float sensibility = 1.0f;
    [HideInInspector]
    public List<string> resolutionsNames = new List<string>();
    [HideInInspector]
    public int initialResolutionIndex = 0;
    [HideInInspector]
    public bool fullscreen = true;
    [HideInInspector]
    public int quality;

    public AudioMixer audioMixer;

    List<Resolution> resolutions = new List<Resolution>();


    void Awake() {
        DontDestroyOnLoad(gameObject);

        if (PlayerPrefs.HasKey("soundsVolume")) soundsVolume = PlayerPrefs.GetFloat("soundsVolume");
        SetSoundsVolume(soundsVolume);
        if (PlayerPrefs.HasKey("musicVolume")) musicVolume = PlayerPrefs.GetFloat("musicVolume");
        SetMusicVolume(musicVolume);
        if (PlayerPrefs.HasKey("sensibility")) sensibility = PlayerPrefs.GetFloat("sensibility");
        SetSensibility(sensibility);
        if (PlayerPrefs.HasKey("quality")) quality = PlayerPrefs.GetInt("quality");
        SetQuality(quality);
        if (PlayerPrefs.HasKey("fullscreen")) fullscreen = System.Convert.ToBoolean(PlayerPrefs.GetInt("fullscreen"));
        SetFullscreen(fullscreen);

        foreach (Resolution r in Screen.resolutions) {
            if(r.refreshRate == Screen.currentResolution.refreshRate)
                resolutions.Add(r);
        }

        for(int i =0; i < resolutions.Count; i++) {
            resolutionsNames.Add(resolutions[i].width + " x " + resolutions[i].height);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                initialResolutionIndex = i;
        }

        if (PlayerPrefs.HasKey("resolution")) initialResolutionIndex = PlayerPrefs.GetInt("resolution");
    }

    public void SetMusicVolume(float value) { 
        musicVolume = value;
        PlayerPrefs.SetFloat("musicVolume", value);
 
        if (value <= 0.0001f) {
            audioMixer.SetFloat("volume", Mathf.Log10(0.0001f) * 20);
        } else if(value == 1.0f) {
            audioMixer.SetFloat("volume", Mathf.Log10(0.9999f) * 20);
        } else {
            audioMixer.SetFloat("volume", Mathf.Log10(value) * 20);
        }
    }

    public void SetSoundsVolume(float value) { 
        soundsVolume = value;
        PlayerPrefs.SetFloat("soundsVolume", value);
    }

    public void SetSensibility(float value) { 
        sensibility = value;
        PlayerPrefs.SetFloat("sensibility", value);
    }

    public void SetQuality (int value) {
        PlayerPrefs.SetInt("quality", value);
        QualitySettings.SetQualityLevel(value);
    }

    public void SetFullscreen (bool value) { 
        Screen.fullScreen = value;
        fullscreen = value;
        PlayerPrefs.SetInt("fullscreen", System.Convert.ToInt32(value));
    }

    public void SetResolution(int value) {
        Resolution resolution = resolutions[value];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("resolution", value);
    }
}
