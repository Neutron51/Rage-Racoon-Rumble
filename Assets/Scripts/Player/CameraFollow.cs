using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Player;

    float camOffsetZ;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camOffsetZ = gameObject.transform.position.z - Player.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 m_cameraPos = new Vector3(Player.position.x, gameObject.transform.position.y, Player.position.z + camOffsetZ);

        gameObject.transform.position = m_cameraPos;
    }
}
