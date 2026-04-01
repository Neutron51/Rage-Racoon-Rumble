using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour {
    public Transform Player;
    public int NumberOfEnemiesToSpawn = 6;
    public float SpawnDelay = 1f;
    public List<Enemy> EnemyPrefabs = new List<Enemy>();
    public SpawnMethod EnemySpawnMethod = SpawnMethod.RoundRobin; // Dictate the method they will spawn

    private Dictionary<int, ObjectPool> EnemyObjectPools = new Dictionary<int, ObjectPool>();

    public void Awake() {
        for (int i = 0; i < EnemyPrefabs.Count; i++) {
            EnemyObjectPools.Add(i, ObjectPool.CreateInstance(EnemyPrefabs[i], NumberOfEnemiesToSpawn));
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
        PoolableObject poolableObject = EnemyObjectPools[spawnIndex].GetObject();

        if (poolableObject != null) {
            Enemy enemy = poolableObject.GetComponent<Enemy>();

            NavMeshTriangulation Triangulation = NavMesh.CalculateTriangulation();

            int vertexIndex = Random.Range(0, Triangulation.vertices.Length);

            NavMeshHit Hit;
            if (NavMesh.SamplePosition(Triangulation.vertices[vertexIndex], out Hit, 2f, 0)) {
                enemy.Agent.Wrap(Hit.position);
                // enemy needs to get enabled and start chasing now.
                enemy.Movement.Player = Player;
                enemy.Agent.enabled = true;
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
