using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    public int maxHealth = 100;
    public int minDamage = 0;
    public int currentHealth;

    public GameObject[] hearts; // array of heart GameObjects

    EnemyController enemyController;

    [SerializeField] private GameObject DeathScreen;

    void Start() {
        currentHealth = maxHealth; // Start at 100
    }

    /* void Update() {
        for (int i = currentHealth; i < hearts.Length; i-- ) {
            if (i < 80) {
                i--; 
            }
            if (i < 60) {
                i--;
            }
            if (i < 40) {
                i--;
            }
            if (i < 20) {
                i--;
            }
            if (i < 0) {
                Destroy(gameObject);
            }

            else {
                Debug.Log("Health not changed");
            }
        }
    } */

    public void TakeDamage(int amount) {
        currentHealth -= amount;
        if (currentHealth < 80) {
            Destroy(hearts[4]);
            Debug.Log($"You got {hearts.Length} hearts left!");
        }
        if (currentHealth < 60) {
            Destroy(hearts[3]);
            Debug.Log($"You got {hearts.Length} hearts left!");
        }
        if (currentHealth < 40) {
            Destroy(hearts[2]);
            Debug.Log($"You got {hearts.Length} hearts left!");
        }
        if (currentHealth < 20) {
            Destroy(hearts[1]);
            Debug.Log($"You got {hearts.Length} hearts left!");
        }
        if (currentHealth < minDamage) {
            Destroy(hearts[0]);
            Debug.Log($"You got {hearts.Length} hearts left!");

            Destroy(gameObject);
            DeathScreen.SetActive(false);
        }

        Debug.Log($"Current health: {currentHealth} HP");
    }

}
