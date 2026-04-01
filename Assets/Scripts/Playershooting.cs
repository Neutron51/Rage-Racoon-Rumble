using UnityEngine;

public class Playershooting : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject Fire;
    public GameObject HitPoint;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shooting();
            Debug.Log("Shoot!");
        }
    }

    void Shooting()
    {
        RaycastHit hit;

        if(Physics.Raycast(FirePoint.position, transform.TransformDirection(Vector3.forward), out hit, 100))
        {
            Debug.DrawRay(FirePoint.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.orange);

            //GameObject a = Instantiate(Fire, FirePoint.position, Quaternion.identity);
            //GameObject b = Instantiate(HitPoint, hit.point, Quaternion.identity);

            //Destroy(a, 1);
            //Destroy(b, 1);

            Enemy enemy = hit.transform.GetComponent<Enemy>();

            if(enemy != null)
            {
                enemy.Damage(15);
            }
        }
    }
}
