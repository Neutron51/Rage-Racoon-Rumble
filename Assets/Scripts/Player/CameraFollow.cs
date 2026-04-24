using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Player;

    float camOffsetX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camOffsetZ = gameObject.transform.position.x - Player.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 m_cameraPos = new Vector3(Player.position.x + camOffsetZ, gameObject.transform.position.y, Player.position.z);

        gameObject.transform.position = m_cameraPos;
    }
}
