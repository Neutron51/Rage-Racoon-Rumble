using System.Collections;
using UnityEngine;
using UnityEditor.Animations;
using UnityEngine.AI;
using JetBrains.Annotations;

public class EnemyController : MonoBehaviour {
    [SerializeField] private EnemyData enemyData;

    public NavMeshAgent agent; 
    public Transform Player;
    public LayerMask whatIsGround, whatIsPlayer;
    public int health;
    public float speed;

    [Header("Patrolling")]
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    [Header("Attacking")]
    public float timeBetweenAttacks;
    public int damageDealt;
    bool alreadyAttacked;
    public GameObject projectile;

    [Header("States")]
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    [Header("Animator")]
    [SerializeField] private AnimatorController animController;
    [SerializeField] private Animator anim;

    [Header("Player")]
    [SerializeField] public PlayerHealth playerHealth;
    

    private void Start() {
        // anim = GetComponent<Animator>(); // get animator at starts 
    }

    private void Awake() {
        Player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();

        // Load values from EnemyData
        if (enemyData != null) {
            agent.speed = enemyData.speed;
            health = enemyData.health;
            sightRange = enemyData.sightRange;
            attackRange = enemyData.attackRange;
            timeBetweenAttacks = enemyData.timeBetweenAttacks;

            walkPointRange = enemyData.walkPointRange;
            projectile = enemyData.projectilePrefab;
        }
    }

    private void Update() {
        // Check for sign and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }
    private void Patroling() {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint() {
        // Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer() {
        agent.SetDestination(Player.position);
    }

    private void AttackPlayer() {
        // Make sure enemy doesn;t move
        agent.SetDestination(transform.position);

        transform.LookAt(Player);

        anim.SetTrigger("Attacking");

        if (!alreadyAttacked) {
            // Attack Code here
            // Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();

            /* rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse); */

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

        playerHealth.TakeDamage(damageDealt);
    }

    private void ResetAttack() {
        // left at https://youtu.be/UjkSFoLxesw?t=220
        alreadyAttacked = false;
    }

    public void TakenDamage(int damage) {
        health -= damage;

        anim.SetTrigger("Damaged");

        if (health <= 0) {
            RoundController rc = FindFirstObjectByType<RoundController>();

            anim.SetBool("isDead", true);

            if (rc != null) {
                rc.DecreaseEnemyCount();
            }

            Destroy(gameObject, 0.95f);
        };
    }

    /* private void DestroyEnemy() {
        Destroy(gameObject);
    } */

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    // ---- DEATH ---

    /* void OnDestroy() {
        RoundController rc = FindFirstObjectByType<RoundController>();

        anim.SetBool("isDead", true);

        if (rc != null) {
            rc.DecreaseEnemyCount();
        }
    } */
}
