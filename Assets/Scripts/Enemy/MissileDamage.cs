using Unity.Jobs;
using Unity.VisualScripting;
using UnityEngine;

public class MissileDamage : MonoBehaviour {
    private int damage;

    void Awake() {
        Destroy(gameObject, 5f);
    }

    public void SetDamage(int dmg) {
        damage = dmg;
    }

    public void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
           PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

           if (playerHealth != null) {
                playerHealth.TakeDamage(damage);
                Debug.Log($"Rambly got smoked for {damage}");
            }
           
           Destroy(gameObject);
        }
    }


}
