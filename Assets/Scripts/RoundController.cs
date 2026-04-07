using System.Collections;
using UnityEngine;

public class RoundController : MonoBehaviour {

    public float spawnRate = 1.0f;
    public float timeBetweenWaves = 4.0f;

    public int enemyCount;

    public GameObject enemy;

    bool waveIsDone = true;

    // VIDEO: https://www.youtube.com/watch?v=4Y05Gbq7GG8&list=PLRRnET3ZAhEzXYJq7I4_RzzHGSBbWlE4D

    // Update is called once per frame
    void Update() {
        if (waveIsDone == true) {
            StartCoroutine(waveSpawner());
        }
    }

    IEnumerator waveSpawner() {
        for (int i = 0; i < enemyCount; i++) {
            GameObject enemyClone = Instantiate(enemy);

            yield return new WaitForSeconds(spawnRate);
        }

        spawnRate -= 0.1f;
        enemyCount += 5;

        yield return new WaitForSeconds(timeBetweenWaves);
    }
}
