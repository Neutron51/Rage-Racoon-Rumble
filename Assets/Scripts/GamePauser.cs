using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePauser : MonoBehaviour {
    [SerializeField] GameObject PauseMenu;
    [SerializeField] private GameObject PauseSign;
    [SerializeField] private GameObject OptionsPanel;
    [SerializeField] private string MainMenuScene;
    [Tooltip("Insert Name of Main Menu Scene Here")] 
    [SerializeField] bool paused = false;

    // AUDIO CONTROLLER
    private GameObject audioController;
    private AudioController audioControllerVar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        PauseMenu.SetActive(false);
        OptionsPanel.SetActive(false);
        audioController = GameObject.FindWithTag("Audio");
        audioControllerVar = audioController.GetComponent<AudioController>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseMenu.SetActive(!PauseMenu.activeInHierarchy);
            paused = !paused;

            if (paused == true) {
                PauseGame();
            }

            if (paused == false) {
                ResumeGame();
            }
        }
    }

    public void OpenOptions() {
        OptionsPanel.SetActive(true);
        PauseSign.SetActive(false);
        audioControllerVar.FindNewSliders();
    }

    public void CloseOptions() {
        OptionsPanel.SetActive(false);
        PauseSign.SetActive(true);
    }

    public void PauseGame() {
        Time.timeScale = 0.0001f;
    }

    public void ResumeGame() {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void MainMenu() {
        SceneManager.LoadScene($"{MainMenuScene}");
    }
}
