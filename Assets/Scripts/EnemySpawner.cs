using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public enum SpawnShape {
        Circle,
        Box
    }
    public int spawnRadius = 0;

    #region NavMesh Spawner

    public SpawnShape spawnShape = SpawnShape.Circle;
    public Vector2 boxSize = new Vector2(0, 0);
    public Vector3 spawningOffset = new Vector3(0, 0, 0);
    
    public int xPos = 50;
    public int zPos = 50;
    #endregion

    [SerializeField]
    private GameObject swarmerPrefab;
    [SerializeField]
    private float swarmerInterval = 3.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        StartCoroutine(spawnEnemy(swarmerInterval, swarmerPrefab));
    }

    public void OnGizmosSelected() {
        Gizmos.color = Color.yellow;
        switch(spawnShape) {
            case SpawnShape.Circle:
                 Gizmos.DrawWireSphere(transform.position, spawnRadius);
                 break;
            case SpawnShape.Box:
                Vector3 size = new Vector3(boxSize.x, 0, boxSize.y);
                Gizmos.DrawWireCube(transform.position, size);
                break;
            default:
                break;
            
        }
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
