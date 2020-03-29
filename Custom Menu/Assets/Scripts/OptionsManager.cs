using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public float soundsVolume = 1.0f;
    private float musicVolume = 1.0f;
    private float sensibility = 1.0f;
    public Slider musicSlider;
    public Slider effectsSlider;
    public Slider sensibilitySlider;


    void Start() {
        DontDestroyOnLoad(gameObject);
        effectsSlider.value = soundsVolume;
        musicSlider.value = musicVolume;
        sensibilitySlider.value = sensibility;
    }

    public void SetMusicVolume(float value) {
        musicVolume = value;
    }

    public void SetSoundsVolume(float value) {
        soundsVolume = value;
    }
}
