using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy/EnemyData")]
public class EnemyData : ScriptableObject {
    [Header("Movement")]
    public float speed = 5f;
    public float walkPointRange = 20f;

    [Header("Detection")]
    public float sightRange = 50f;
    public float attackRange = 10f;

    [Header("Combat")]
    public int health = 100;
    public int damageDealt = 10;
    public float timeBetweenAttacks = 2f;

    [Header("References")]
    public GameObject projectilePrefab;
}
