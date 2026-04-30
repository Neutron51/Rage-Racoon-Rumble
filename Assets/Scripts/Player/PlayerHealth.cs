using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    public int maxHealth = 100;
    public int minHealth = 0;
    public int currentHealth;

    public GameObject[] hearts; // array of heart GameObjects

    EnemyController enemyController;

    [SerializeField] private GameObject DeathScreen;

    void Start() {
        currentHealth = maxHealth; // Start at 100
    }

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
        if (currentHealth < minHealth) {
            Destroy(hearts[0]);
            Debug.Log($"You got {hearts.Length} hearts left!");

            Destroy(gameObject);
            DeathScreen.SetActive(true);
        }

        Debug.Log($"Current health: {currentHealth} HP");
    }

}
