using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour {

    // tutorial: https://www.youtube.com/watch?v=G-JUp8AMEx0

    [SerializeField] public AudioMixer audioMixer;
    [SerializeField] public Slider mainSlider;
    [SerializeField] public Slider musicSlider;
    [SerializeField] public Slider sfxSlider;

    private static AudioController audioInstance;
    private bool IsLoaded = false;

    void Awake() {
        DontDestroyOnLoad(this.gameObject);

        if (audioInstance == null) {
            audioInstance = this;
        }
        else {
            UnityEngine.Object.Destroy(gameObject);
        }
    }   

    void Start() {
        if (IsLoaded == false) {
        if (PlayerPrefs.HasKey("MusicVolume")) {
            LoadVolume();
        }
        else {
            SetMusicVolume();
        }
        IsLoaded = true;
      }
        else {
           return; 
        }

        /* mainSlider.onValueChanged.AddListener (delegate {SetMainVolume();});
        musicSlider.onValueChanged.AddListener (delegate {SetMusicVolume();});
        sfxSlider.onValueChanged.AddListener (delegate {SetSfxVolume();}); */
    }

    public void FindNewSliders() {
        if (mainSlider == null) mainSlider = GameObject.Find("MainSlider").GetComponent<Slider>();
        if (musicSlider == null) musicSlider = GameObject.Find("MusicSlider").GetComponent<Slider>();
        if (sfxSlider == null) sfxSlider = GameObject.Find("SfxSlider").GetComponent<Slider>();

        // ADD LISTENERS

        mainSlider.onValueChanged.AddListener (delegate {SetMainVolume();});
        musicSlider.onValueChanged.AddListener (delegate {SetMusicVolume();});
        sfxSlider.onValueChanged.AddListener (delegate {SetSfxVolume();});

        Debug.Log("Finding new Sliders!");
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
