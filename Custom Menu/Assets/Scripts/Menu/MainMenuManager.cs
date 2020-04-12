﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField] float fadeTime = 2f;
    public Image fadePanel;
    public GameObject m_panelAnyKey;
    public GameObject m_panelMenu;

    public Scrollbar musicSlider;
    public Scrollbar soundsSlider;
    public Scrollbar sensibilitySlider;
    public TMPro.TMP_Dropdown resolutionDropdown;
    public TMPro.TMP_Dropdown qualityDropdown;
    public Toggle fullscreen;

    public SplineController splineController;

    private bool waitAnyKey = true;

    float counter = 72f;

    GameManager gameManager;
    Settings settings;

    AudioSource[] bgMusic;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        settings = FindObjectOfType<Settings>();
        bgMusic = FindObjectsOfType<AudioSource>();

        soundsSlider.value = settings.soundsVolume;
        musicSlider.value = settings.musicVolume;
        sensibilitySlider.value = settings.sensibility;
        fullscreen.isOn = settings.fullscreen;

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(settings.resolutionsNames);
        resolutionDropdown.value = settings.initialResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        qualityDropdown.value = settings.quality;

        StartCoroutine(UnFading());
    }

    public void SetSoundsVolume(float value) { settings.SetSoundsVolume(value); }
    public void SetMusicVolume(float value) { settings.SetMusicVolume(value); }
    public void SetSensibility(float value) { settings.SetSensibility(value); }
    public void SetQuality(int value) { settings.SetQuality(value); }
    public void SetFullscreen(bool value) { settings.SetFullscreen(value); }
    public void SetResolution(int value) { settings.SetResolution(value); }

    void Update()
    {

        counter += Time.deltaTime;
        if (counter > 75f)
        {
            counter = 0;
            splineController.FollowSpline();
        }

        if (waitAnyKey)
        {
            if (Input.anyKey/* || Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire2") || Input.GetButtonDown("Exit")*/)
            {
                waitAnyKey = false;
                m_panelAnyKey.SetActive(false);
                m_panelMenu.SetActive(true);
            }
        }
    }

    public void Play() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);}

    public void Quit() { Application.Quit();}

    IEnumerator Fading()
    {
        for (float t = 0.0f; t < fadeTime;)
        {
            t += Time.deltaTime;
            fadePanel.color = new Color(0f, 0f, 0f, t / (fadeTime));
            yield return null;
        }
        m_panelAnyKey.gameObject.SetActive(true);
    }
    IEnumerator UnFading()
    {
        for (float t = fadeTime; t > 0.0f;)
        {
            t -= Time.deltaTime;
            fadePanel.color = new Color(0f, 0f, 0f, t / (fadeTime));
            yield return null;
        }
    }
}
