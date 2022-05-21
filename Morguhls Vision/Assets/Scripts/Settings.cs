using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    Resolution[] resolutions;
    public Dropdown ResDropdown;
    public Dropdown QualDropdown;
    [SerializeField] GameObject VideoSettings;
    [SerializeField] GameObject AudioSettings;
    [SerializeField] GameObject KeybindeSettings;
    public AudioMixer audioMixer;
    [SerializeField] AudioSource audioSource;
    public static Settings Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        resolutions = Screen.resolutions;
        ResDropdown.ClearOptions();

        int currentRes = 0;
        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentRes = i;
            }
        }
        ResDropdown.AddOptions(options);
        ResDropdown.value = currentRes;
        ResDropdown.RefreshShownValue();
        QualDropdown.value = QualitySettings.GetQualityLevel();
    }

    public void SetResolution(int resIndex)
    {
        Resolution resolution = resolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
    public void SetSound(bool isSound)
    {
        audioSource.enabled = isSound;
    }

    public void ShowVideo()
    {
        AudioSettings.SetActive(false);
        KeybindeSettings.SetActive(false);
        VideoSettings.SetActive(true);
    }
    public void ShowAudio()
    {
        VideoSettings.SetActive(false);
        KeybindeSettings.SetActive(false);
        AudioSettings.SetActive(true);
    }
    public void ShowKeybind()
    {
        AudioSettings.SetActive(false);
        VideoSettings.SetActive(false);
        KeybindeSettings.SetActive(true);
    }
}
