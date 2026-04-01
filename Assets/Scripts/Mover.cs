using UnityEngine;
namespace TopDown.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class Mover : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;
        protected Vector3 currentInput;
        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            rb.linearVelocity = movementSpeed * currentInput * Time.fixedDeltaTime;
        }
    }
}

