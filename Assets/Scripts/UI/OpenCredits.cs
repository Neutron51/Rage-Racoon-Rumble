using System;
using Unity.Collections;
using UnityEditor.Animations;
using UnityEditor.Search;
using UnityEngine;

public class OpenCredits : MonoBehaviour {

    [SerializeField] public Vector3 initialPosition;
    [SerializeField] public Vector3 newPosition; 
    [SerializeField] public Camera CameraObj;
    [SerializeField] public float CameraX;
    [SerializeField] public float CameraY;

    [SerializeField] private AnimatorController animController;
    private Animator anim;

    private bool Responsibility;

    void Start() {
        anim = CameraObj.GetComponent<Animator>();
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

        // CameraObj.transform.position = newPosition;
        Debug.Log("Credits Clicked!");
    }

    public void CloseCredits() {
        anim.SetBool("Credits", false);

        Responsibility = false;
        
        // CameraObj.transform.position = initialPosition;
        Debug.Log("Closed Credits");
    }
}
