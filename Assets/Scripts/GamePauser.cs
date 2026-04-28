using System;
using UnityEngine;

public class GamePauser : MonoBehaviour {
    [SerializeField] GameObject PauseMenu;
    [SerializeField] private GameObject PauseSign;
    [SerializeField] private GameObject OptionsPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        PauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseMenu.SetActive(true);
        }
    }

    void OpenOptions() {
        OptionsPanel.SetActive(true);
        PauseSign.SetActive(false);
    }

    void CloseOptions() {
        OptionsPanel.SetActive(false);
        PauseSign.SetActive(true);
    }

    void ResumeGame() {
        PauseMenu.SetActive(false);
    }
}
