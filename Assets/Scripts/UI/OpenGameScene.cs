using UnityEditor.Search;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicClick : MonoBehaviour {

    [SerializeField] private string SceneName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnMouseUpAsButton() {
        SceneManager.LoadScene($"{SceneName}");
    }

    public void OpenScene(string sceneClickName) {
        SceneManager.LoadScene($"{sceneClickName}");
    }
}
