using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RoundController : MonoBehaviour {

    public enum SpawnState { spawning, waiting, counting };
    public SpawnState state = SpawnState.counting;

    // following this tutorial: https://www.youtube.com/watch?v=Vrld13ypX_I&list=PLRRnET3ZAhEzXYJq7I4_RzzHGSBbWlE4D&index=4

    [SerializeField] TextMeshProUGUI enemyCountText;
    [SerializeField] TextMeshProUGUI roundCount;

    [System.Serializable] // This tells Unity that these params can be changed inside of the editor
    public class Wave {
        public string name;
        public Transform enemy;
        public int enemyCount;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public float timeBetweenWaves = 5f;
    public float waveCountdown = 0f;

    void Start() {
        waveCountdown = timeBetweenWaves;
    }


    void Update() {
        if (waveCountdown <= 0) {
            if (state != SpawnState.spawning) {
                // Start spawning wave
                StartCoroutine(SpawnWave ( waves[nextWave] ));
            }
        }
        else {
            waveCountdown -= Time.deltaTime;
        }
    }

    IEnumerator SpawnWave (Wave _wave) { // IEnumeratos always
        state = SpawnState.spawning;

        for (int i = 0; i < _wave.enemyCount; i++) {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds( 1f/_wave.rate );
        }

        // Spawn

        state = SpawnState.waiting; // we are waiting fdor t he player to kill of all of the enemies

        yield break;
    }

    void UpdateTextGUI() {
        enemyCountText.text = $"Count: {enemyCount.Count()}";
        roundCount.text = $"Round: {waves.Length}";
    }

    void SpawnEnemy (Transform _enemy) {
        // spawn enemy
        Debug.Log("Spawning Enemy: " + _enemy.name);
    }
}
