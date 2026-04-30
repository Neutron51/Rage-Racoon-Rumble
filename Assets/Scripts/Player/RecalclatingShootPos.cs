using UnityEngine;
using UnityEngine.UIElements;

public class RecalclatingShootPos : MonoBehaviour
{
    public Transform shootRotation;
    public Transform playerRotation;

    // Update is called once per frame
    void Update()
    {
        shootRotation.rotation = playerRotation.rotation;
    }
}
