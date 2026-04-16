using UnityEngine;

public class RPGFiring : MonoBehaviour
{
    public bool IsFiring;

    public Missiles bullet;
    public float missileSpeed;

    public float timeBetweenShots;
    private float shotCounter;

    public Transform firePoint;
    public RPGFiring rpg;

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
                Missiles newMissile = Instantiate(bullet, firePoint.position, firePoint.rotation) as Missiles;
                newMissile.speed = missileSpeed;
            }
        }
        else
        {
            shotCounter = 0;
        }
        
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            rpg.IsFiring = true;
        }

        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            rpg.IsFiring = false;
        }
    }
}
