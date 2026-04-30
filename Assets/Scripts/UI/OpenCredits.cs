using UnityEditor.Animations;
using UnityEngine;

public class OpenCredits : MonoBehaviour {

    [SerializeField] public Vector3 initialPosition;
    [SerializeField] public Vector3 newPosition; 
    [SerializeField] public Camera CameraObj;
    [SerializeField] public float CameraX;
    [SerializeField] public float CameraY;
    [SerializeField] private GameObject SodaCans;

    [SerializeField] private AnimatorController animController;
    private Animator anim;

    private bool Responsibility;

    void Start() {
        anim = CameraObj.GetComponent<Animator>();
        SodaCans.SetActive(false);
    }

    void Update() {
        if (Responsibility == true) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                CloseCredits();
            }
        }
    }

    void Awake() {
        initialPosition = new Vector3(0, 1, -10); 
        CameraObj.transform.position = initialPosition;
    }
    private void OnMouseUpAsButton() {
        anim.SetBool("Credits", true);

        Responsibility = true;
        SodaCans.SetActive(true);

        // CameraObj.transform.position = newPosition;
        Debug.Log("Credits Clicked!");
    }

    public void CloseCredits() {
        anim.SetBool("Credits", false);

        Responsibility = false;
        SodaCans.SetActive(false);
        
        // CameraObj.transform.position = initialPosition;
        Debug.Log("Closed Credits");
    }
}
