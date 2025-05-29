using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("MasterVolume") && PlayerPrefs.HasKey("musicVolume") && PlayerPrefs.HasKey("SFXVolume"))
        {
            LoadVolume();
        }
        else
        {
            masterSlider.value = 0.2f;
            bgmSlider.value = 0.2f;
            sfxSlider.value = 0.2f;
            SetMasterVolume();
            SetMusicVolume();
            SetSFXVolume();
        }

        //masterSlider.onValueChanged.AddListener(delegate { SetMasterVolume(); });
        //bgmSlider.onValueChanged.AddListener(delegate { SetMusicVolume(); });
        //sfxSlider.onValueChanged.AddListener(delegate { SetSFXVolume(); });
    }

    public void SetMasterVolume()
    {
        float masterVolume = masterSlider.value;
        myMixer.SetFloat("Master", Mathf.Log10(masterVolume) * 20);
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        //ApplyVolumeSettings();
    }

    public void SetMusicVolume()
    {
        //ApplyVolumeSettings();
        float volume = bgmSlider.value;
        myMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", bgmSlider.value);
    }

    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
    }

    //private void ApplyVolumeSettings()
    //{
    //    float masterVolume = masterSlider.value;
    //    float musicVolume = bgmSlider.value * masterVolume;
    //    float sfxVolume = sfxSlider.value * masterVolume;

    //    myMixer.SetFloat("BGM", Mathf.Log10(musicVolume) * 20);
    //    myMixer.SetFloat("SFX", Mathf.Log10(sfxVolume) * 20);
    //}

    private void LoadVolume()
    {
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.2f);
        bgmSlider.value = PlayerPrefs.GetFloat("musicVolume", 0.2f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.2f);
        SetMasterVolume();
        SetMusicVolume();
        SetSFXVolume();
        //ApplyVolumeSettings();
    }
}
