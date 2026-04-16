    using System;
    using System.Collections;
using System.Collections.Generic;
using TMPro;
    using Unity.VisualScripting;
    using UnityEditor.ShaderKeywordFilter;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class RoundController : MonoBehaviour {

        public enum SpawnState { spawning, waiting, counting };
        public SpawnState state = SpawnState.counting;

        // following this tutorial: https://www.youtube.com/watch?v=Vrld13ypX_I&list=PLRRnET3ZAhEzXYJq7I4_RzzHGSBbWlE4D&index=4

        [SerializeField] TextMeshProUGUI enemyCountText;
        [SerializeField] TextMeshProUGUI roundCount;

        [System.Serializable] // This tells Unity that these params can be changed inside of the editor
        public class Wave {
            public string name;
            public GameObject enemy;
            public int enemyCount;
            public float rate;
        }
        
        public List<EnemyController> enemyList;

        private int aliveEnemies = 0; // just a counter, this number does not "kill" enemies
        public Wave[] waves;
        private int nextWave = 0;

        public float timeBetweenWaves = 5f;
        public float waveCountdown = 0f;



        // ---- START A WAVE COUNTDOWN ONCE THE GAME STARTS ----

        void Start() {
            waveCountdown = timeBetweenWaves;
        }


        void Update() {
            if (waveCountdown <= 0) {
                if (state == SpawnState.counting) {
                    // Start spawning wave
                    StartCoroutine(SpawnWave(waves[nextWave]));
                }
            }
            else {
                waveCountdown -= Time.deltaTime;
            }

            // kill an enemy when you press the F key

            if (Keyboard.current.fKey.wasPressedThisFrame && aliveEnemies > 0) {
                EnemyController ec = enemyList[enemyList.Count - 1];

                ec.TakenDamage(999);
                enemyList.RemoveAt(enemyList.Count - 1);
                // objective, make enemies = enemies inside of enemyList


                if (enemyList.Count > 0) {
                    Debug.Log("Enemy eliminated!");
                }
                else {
                    Debug.Log("no enemies to kill! 🫡");
                }   
            }

            // *-- COMPLETE WAVE IF ENEMY COUNT IS BELOW 0 --* 

            if (state == SpawnState.waiting && aliveEnemies <= 0) {
                if (aliveEnemies <= 0){
                    WaveCompleted();
                }
            }
        }

        // ---- START THE WAVE! ----

        IEnumerator SpawnWave (Wave _wave) { // IEnumeratos always  
            waveCountdown = timeBetweenWaves;
            state = SpawnState.spawning;
            Debug.Log("Starting Wave!");

            for (int i = 0; i < _wave.enemyCount; i++) {
                SpawnEnemy(_wave.enemy);
                yield return new WaitForSeconds( 1f/_wave.rate );
            }

            // Spawn
            Debug.Log($"using Prefab: {_wave.enemy}");

            state = SpawnState.waiting; // we are waiting for the player to kill of all of the enemies
            yield break;
        }

        // ---- UPDATE THE TEXT ----

        void UpdateTextGUI(Wave enemywave) {
            enemyCountText.text = $"Enemies Left: {aliveEnemies}";
            roundCount.text = $"Round: {waves.Length}";
        }

        #region Spawn and Kill Enemies

        // ---- SPAWN ENEMIES ----

        void SpawnEnemy (GameObject _enemy) {
            if (_enemy == null) {
                Debug.Log("Yo! The Prefab is missing!");
                return;
            }

            Vector3 spawnPos = new Vector3(UnityEngine.Random.Range(-10,10), 0, UnityEngine.Random.Range(-10, 10));

            GameObject spawned = Instantiate(_enemy, spawnPos, Quaternion.identity);
            aliveEnemies++;

            // add enemy to list
            enemyList.Add(spawned.GetComponent<EnemyController>());

            // spawn enemy
            Debug.Log($"Spawning Enemy: {_enemy.name}, Alive enemies: {aliveEnemies}");
        }

        public void DecreaseEnemyCount() {
            aliveEnemies--;
            Debug.Log($"Enemy dead. there are {aliveEnemies} enemies Remaining!");
        }

        #endregion

        #region Wave Completed

        // ---- WAVE COMPLETED -----

        void WaveCompleted() {
            Debug.Log("Wave completed!");

            state = SpawnState.counting;
            waveCountdown = timeBetweenWaves;

            nextWave++;

            if (nextWave >= waves.Length) {
                nextWave = 0;
                Debug.Log("All waves complete!");
                state = SpawnState.waiting; // stop the spawn loop from restarting
                Debug.Log($"Next wave index: {nextWave}");
                return; // do not reset nextWave // return terminates any function at that point
            }
        }

        #endregion
    }
