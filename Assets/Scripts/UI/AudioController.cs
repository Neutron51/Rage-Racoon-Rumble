using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour {

    // tutorial: https://www.youtube.com/watch?v=G-JUp8AMEx0

    [SerializeField] public AudioMixer audioMixer;
    [SerializeField] private Slider mainSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start() {
        if (PlayerPrefs.HasKey("MusicVolume")) {
            LoadVolume();
        }
        else {
            SetMusicVolume();
        }
    }

    public void SetMainVolume() {
        float volume = mainSlider.value;
        audioMixer.SetFloat("main", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("MainVolume", volume);
    }

    public void SetMusicVolume() {
        float volume = musicSlider.value;
        audioMixer.SetFloat("music", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSfxVolume() {
        float volume = sfxSlider.value;
        audioMixer.SetFloat("sfx", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("SfxVolume", volume);
    }

    private void LoadVolume() {
        mainSlider.value = PlayerPrefs.GetFloat("MainVolume");
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("SfxVolume");

        SetMusicVolume();
    }
}
