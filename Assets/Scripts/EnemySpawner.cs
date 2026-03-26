using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField]
    private GameObject swarmerPrefab;

    [SerializeField]
    private float swarmerInterval = 3.5f;

    public int xPos = 50;
    public int zPos = 50;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        StartCoroutine(spawnEnemy(swarmerInterval, swarmerPrefab));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy) {
        xPos = Random.Range(-16, 13);
        zPos = Random.Range(-20, 1);
        Instantiate(swarmerPrefab, new Vector3 (xPos, 1, zPos), Quaternion.identity);
        yield return new WaitForSeconds(interval); // wait for the end of interval
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f, 5f), Random.Range(-6f, 6f), 0), Quaternion.identity);
        // define newEnemy
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}
