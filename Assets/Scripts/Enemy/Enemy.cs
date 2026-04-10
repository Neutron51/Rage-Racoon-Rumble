using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Health = 100;
    public int EnemySpeed;
    GameObject m_player;

    void Awake()
    {
        m_player = GameObject.Find("Player");
    }

    void Update()
    {
        Vector3 localPosition = m_player.transform.position - transform.position;
        localPosition = localPosition.normalized;
        transform.Translate(localPosition.x * Time.deltaTime * EnemySpeed, 0f, localPosition.z * Time.deltaTime * EnemySpeed);
    }

    public void Damage(int damage)
    {
        Health -= damage;

        if(Health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
