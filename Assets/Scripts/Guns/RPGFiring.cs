using UnityEngine;

public class RPGFiring : MonoBehaviour
{
    public bool IsFiring;

    public Missile bullet;
    public float missileSpeed;

    public float timeBetweenShots;
    private float shotCounter;

    public Transform firePoint;

    private void Start()
    {
        
    }

    private void Update()
    {
        if(IsFiring)
        {
            shotCounter -= Time.deltaTime;
            if(shotCounter <= 0)
            {
                shotCounter = timeBetweenShots;
                Missile newMissile = Instantiate(bullet, firePoint.position, firePoint.rotation) as Missile;
                newMissile.speed = missileSpeed;
            }
        }
        else
        {
            shotCounter = 0;
        }
    }
}
