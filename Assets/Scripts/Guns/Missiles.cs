using UnityEngine;

public class Missiles : MonoBehaviour
{
    public GameObject rocketPrefab;
    public int Damage;

        private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyController>().TakenDamage(Damage);
            Destroy(rocketPrefab);
        }
    }
}
