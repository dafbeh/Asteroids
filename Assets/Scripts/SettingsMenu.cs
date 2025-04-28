using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resDropdown;
    [SerializeField] private Slider volSlider;
    [SerializeField] private Toggle musicTog;

    Resolution[] resolutions;
    private IAudioSystem audioSystem;

    void Awake()
    {
        audioSystem = ServiceLocator.Get<IAudioSystem>();
    }

    void Start()
    {
        volSlider.value = PlayerPrefs.GetFloat("Volume", 0.5f);
        musicTog.isOn = PlayerPrefs.GetInt("music", 1) == 1;

        resolutions = Screen.resolutions;
        resDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++) {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height) {
                    currentResolutionIndex = i;
            }
        }

        resDropdown.AddOptions(options);
        resDropdown.value = currentResolutionIndex;
        resDropdown.RefreshShownValue();
    }

    public void setRes(int resIndex) {
        audioSystem.PlaySound("Sounds/menu");
        Resolution resolution = resolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        PlayerPrefs.SetInt("ResolutionWidth", resolution.width);
        PlayerPrefs.SetInt("ResolutionHeight", resolution.height);
        PlayerPrefs.Save();
    }
 
    public void setVolume(float vol) {
        AudioListener.volume = vol;

        PlayerPrefs.SetFloat("Volume", vol);
        PlayerPrefs.Save();
    }

    public void SetFullscreen(bool isFullScreen) {
        Screen.fullScreen = isFullScreen;

        PlayerPrefs.SetInt("Fullscreen", isFullScreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void toggleMusic() {
        bool check = musicTog.isOn;

        PlayerPrefs.SetInt("music", check ? 1 : 0);
        PlayerPrefs.Save();

        if(check) {
            audioSystem.PlaySound("Sounds/AdaptiveMusic/Ambient");
        } else {
            audioSystem.StopSound("Sounds/AdaptiveMusic/Ambient");
        }
    }

    public void playClick() {
        audioSystem.PlaySound("Sounds/menu");
    }
}