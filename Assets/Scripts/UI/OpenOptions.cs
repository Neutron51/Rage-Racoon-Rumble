// using UnityEditor.Animations;
using UnityEngine;

public class OpenOptions : MonoBehaviour {

    [SerializeField] public Vector3 initialPosition;
    [SerializeField] public Vector3 newPosition; 
    [SerializeField] public Camera CameraObj;
    [SerializeField] public float CameraX;
    [SerializeField] public float CameraY;

    [SerializeField] private RuntimeAnimatorController animController;
    private Animator anim;

    private bool Responsibility;

    void Start() {
        anim = CameraObj.GetComponent<Animator>();
    }

    void Update() {
        if (Responsibility == true) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                CloseOptions();
            }
        }
    }

    void Awake() {
        initialPosition = new Vector3(0, 1, -10); 
        CameraObj.transform.position = initialPosition;
    }
    private void OnMouseUpAsButton() {
        anim.SetBool("Options", true);

        Responsibility = true;

        // CameraObj.transform.position = newPosition;
        Debug.Log("Options Clicked!");
    }

    public void CloseOptions() {
        anim.SetBool("Options", false);

        Responsibility = false;

        // CameraObj.transform.position = initialPosition;
        Debug.Log("Closed Options");
    }
}
