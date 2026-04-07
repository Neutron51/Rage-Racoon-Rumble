using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour {
    public Transform Player;
    public int NumberOfEnemiesToSpawn = 6;
    public float SpawnDelay = 1f;
    public List<EnemyController> EnemyPrefabs = new List<EnemyController>();
    public SpawnMethod EnemySpawnMethod = SpawnMethod.RoundRobin; // Dictate the method they will spawn

    private Dictionary<int, ObjectPool<EnemyController>> EnemyObjectPools = new Dictionary<int, ObjectPool<EnemyController>>();

    public void Awake() {
        for (int i = 0; i < EnemyPrefabs.Count; i++) {
            int index = i;

            EnemyObjectPools.Add(index, new ObjectPool<EnemyController>(
                () => Instantiate(EnemyPrefabs[index]), // create
                enemy => enemy.gameObject.SetActive(true), // on get
                enemy => enemy.gameObject.SetActive(false), // on release
                enemy => Destroy(enemy.gameObject), // on destroy
                false,
                NumberOfEnemiesToSpawn,
                NumberOfEnemiesToSpawn
            ));
        }
    }

    private void Start() {
        StartCoroutine(SpawnEnemy()); // Coroiutine to spawn enemies
    }

    private IEnumerator SpawnEnemy() {
        WaitForSeconds Wait = new WaitForSeconds(SpawnDelay); // Wait for SpawnDelay

        int SpawnedEnemies = 0;

        while (SpawnedEnemies < NumberOfEnemiesToSpawn) {
            if (EnemySpawnMethod == SpawnMethod.RoundRobin) {
                SpawnRoundRobinEnemy(SpawnedEnemies);
            }
            else if (EnemySpawnMethod == SpawnMethod.Random) {
                SpawnRandomEnemy();
            }

            SpawnedEnemies++;

            yield return Wait;
        }
    }

    private void SpawnRoundRobinEnemy(int SpawnedEnemies) {
        int SpawnIndex = SpawnedEnemies % EnemyPrefabs.Count; // 0 % 2 = 0 %

        DoSpawnEnemy(SpawnIndex);
    }

    private void SpawnRandomEnemy() {
        DoSpawnEnemy(Random.Range(0, EnemyPrefabs.Count));
    }

    private void DoSpawnEnemy(int spawnIndex) {
        EnemyController enemy = EnemyObjectPools[spawnIndex].Get();

        if (enemy != null) {
            // Get a random point of the NavMesh
            NavMeshTriangulation Triangulation = NavMesh.CalculateTriangulation();
            int vertexIndex = Random.Range(0, Triangulation.vertices.Length);
            Vector3 randomPoint = Triangulation.vertices[vertexIndex];

            NavMeshHit Hit;
            if (NavMesh.SamplePosition(randomPoint, out Hit, 5f, NavMesh.AllAreas)) {
                // Disable the agent before wrapping for stability
                enemy.agent.enabled = false;
                enemy.transform.position = Hit.position;
                enemy.agent.enabled = true;
            }
        }
        else {
            Debug.LogError($"Unable to getch enemies of type {spawnIndex} from object pool. Out of object?");
        }
    }

    public enum SpawnMethod {
        RoundRobin,
        Random
    }
}
