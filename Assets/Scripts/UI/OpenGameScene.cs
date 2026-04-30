using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicClick : MonoBehaviour {

    [SerializeField] private string SceneName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnMouseUpAsButton() {
        Time.timeScale = 1f;
        SceneManager.LoadScene($"{SceneName}");
    }

    public void OpenScene(string sceneClickName) {
        Time.timeScale = 1f;
        SceneManager.LoadScene($"{sceneClickName}");
    }
}
