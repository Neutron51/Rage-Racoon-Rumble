using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public int EnemySpeed;
    GameObject m_player;

    void Awake()
    {
        m_player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 localPosition = m_player.transform.position - transform.position;
        localPosition = localPosition.normalized;
        transform.Translate(localPosition.x * Time.deltaTime * EnemySpeed, 0f, localPosition.z * Time.deltaTime * EnemySpeed);
    }
}
