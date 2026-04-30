using UnityEngine;

public class Missiles : MonoBehaviour
{
    public GameObject rocketPrefab;

        private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyController>().TakenDamage(50);
            Destroy(rocketPrefab);
        }
    }
}
