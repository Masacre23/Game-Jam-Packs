using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    private float soundsVolume = 1.0f;
    private float musicVolume = 1.0f;
    private float sensibility = 1.0f;

    public Scrollbar musicSlider;
    public Scrollbar effectsSlider;
    public Scrollbar sensibilitySlider;

    public AudioMixer audioMixer;

    public TMPro.TMP_Dropdown resolutionDropdown;
    List<Resolution> resolutions = new List<Resolution>();


    void Start() {
        DontDestroyOnLoad(gameObject);

        effectsSlider.value = soundsVolume;
        SetSoundsVolume(soundsVolume);
        musicSlider.value = musicVolume;
        SetMusicVolume(musicVolume);
        sensibilitySlider.value = sensibility;
        SetSensibility(sensibility);

        foreach (Resolution r in Screen.resolutions) {
            if(r.refreshRate == Screen.currentResolution.refreshRate)
                resolutions.Add(r);
        }

        resolutionDropdown.ClearOptions();
        List<string> resolutionsNames = new List<string>();

        int currentResolutionIndex = 0;
        for(int i =0; i < resolutions.Count; i++) {
            resolutionsNames.Add(resolutions[i].width + " x " + resolutions[i].height);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }
        resolutionDropdown.AddOptions(resolutionsNames);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetMusicVolume(float value) { 
        musicVolume = value;
 
        if (value <= 0.0001f) {
            audioMixer.SetFloat("volume", Mathf.Log10(0.0001f) * 20);
        } else if(value == 1.0f) {
            audioMixer.SetFloat("volume", Mathf.Log10(0.9999f) * 20);
        } else {
            audioMixer.SetFloat("volume", Mathf.Log10(value) * 20);
        }
    }

    public void SetSoundsVolume(float value) { soundsVolume = value; }

    public void SetSensibility(float value) { sensibility = value; }

    public void SetQuality (int value) { QualitySettings.SetQualityLevel(value); }

    public void SetFullscreen (bool value) { Screen.fullScreen = value; }

    public void SetResolution(int value) {
        Resolution resolution = resolutions[value];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen); 
    }
}
