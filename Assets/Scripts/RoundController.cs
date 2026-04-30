    using System.Collections;
using System.Collections.Generic;
using TMPro;
    using UnityEngine;
    using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RoundController : MonoBehaviour {

        public enum SpawnState { spawning, waiting, counting };
        public SpawnState state = SpawnState.counting;

        // following this tutorial: https://www.youtube.com/watch?v=Vrld13ypX_I&list=PLRRnET3ZAhEzXYJq7I4_RzzHGSBbWlE4D&index=4

        [SerializeField] TextMeshProUGUI enemyCountText;
        [SerializeField] TextMeshProUGUI roundCount;
        [SerializeField] TextMeshProUGUI timerText;

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

        public float timeBetweenWaves = 3f;
        public float waveCountdown = 0f;

        [Header("Victory Screen")]
        [SerializeField] public GameObject victoryScreen;

        [Header("Spawn Locations")]
        [SerializeField] public GameObject[] spawnLoc;

        [Header("GUI")]
        [SerializeField] public Image EnemyIcons;
        [SerializeField] private Sprite Rat;
        [SerializeField] private Sprite Cat;
        [SerializeField] private Sprite Pitbull;
        [SerializeField] private Sprite BigTony;

        [Header("Guns")]
        [SerializeField] private GameObject Watergun;
        [SerializeField] private GameObject Pistol;
        [SerializeField] private GameObject Uzi;
        [SerializeField] private GameObject Rpg;


        // ---- START A WAVE COUNTDOWN ONCE THE GAME STARTS ----

        void Start() {
            waveCountdown = timeBetweenWaves;

            victoryScreen.SetActive(false);

            Watergun.SetActive(true);
            Pistol.SetActive(false);
            Uzi.SetActive(false);
            Rpg.SetActive(false);
            
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

            #if UNITY_EDITOR

            if (Keyboard.current.fKey.wasPressedThisFrame && aliveEnemies > 0) {
                EnemyController ec = enemyList[enemyList.Count - 1];

                ec.TakenDamage(999);
                enemyList.RemoveAt(enemyList.Count - 1);
                // objective, make enemies = enemies inside of enemyList


                if (enemyList.Count > 0) {
                    Debug.Log("Enemy eliminated!");
                }
                else {
                    Debug.Log("no enemies to kill!");
                }   
            }

            #endif

            // Update GUI text

            UpdateTextGUI();

            // *-- COMPLETE WAVE IF ENEMY COUNT IS BELOW 0 --* 

            if (state == SpawnState.waiting && aliveEnemies <= 0) {
                if (aliveEnemies <= 0){
                    Debug.Log("Wave Completed!");
                    WaveCompleted();
                }
            }
        }

        // ---- START THE WAVE! ----

        IEnumerator SpawnWave (Wave _wave) { // IEnumeratos always  
            state = SpawnState.spawning;
            Debug.Log("Starting Wave!");

            for (int i = 0; i < _wave.enemyCount; i++) {
                SpawnEnemy(_wave.enemy);
                // yield return new WaitForSeconds( 1f/_wave.rate );
            }

            // Spawn
            Debug.Log($"using Prefab: {_wave.enemy}");

            // Enemy Icon
            if (nextWave == 0) {
                EnemyIcons.sprite = Rat;
            }
            if (nextWave == 1) {
                EnemyIcons.sprite = Cat;

                Watergun.SetActive(false);
                Pistol.SetActive(true);
                Uzi.SetActive(false);
                Rpg.SetActive(false);
            }
            if (nextWave == 2) {
                EnemyIcons.sprite = Pitbull;

                Watergun.SetActive(false);
                Pistol.SetActive(false);
                Uzi.SetActive(true);
                Rpg.SetActive(false);
            }
            if (nextWave == 3) {
                EnemyIcons.sprite = BigTony;

                Watergun.SetActive(false);
                Pistol.SetActive(false);
                Uzi.SetActive(false);
                Rpg.SetActive(true);
            }
            else {
                Debug.Log("All Rounds Completed!");
            }

            state = SpawnState.waiting; // we are waiting for the player to kill of all of the enemies
            yield break;
        }

        // ---- UPDATE THE TEXT ----

        void UpdateTextGUI() {
            enemyCountText.text = $"{aliveEnemies}";
            roundCount.text = $"Round: {nextWave + 1}";
            timerText.text = $"{waveCountdown.ToString("0.00")} S"; 
        }

        #region Spawn and Kill Enemies

        // ---- SPAWN ENEMIES ----

        void SpawnEnemy (GameObject _enemy) {
            if (_enemy == null) {
                Debug.Log("Yo! The Prefab is missing!");
                return;
            }

            GameObject spawned = Instantiate(_enemy, spawnLoc[UnityEngine.Random.Range(1, spawnLoc.Length)].transform.position, Quaternion.identity);
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

                // show victory screen
                victoryScreen.SetActive(true);
                return; // do not reset nextWave // return terminates any function at that point
            }
        }

        #endregion
    }
