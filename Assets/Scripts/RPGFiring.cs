using UnityEngine;

public class RPGFiring : MonoBehaviour
{
public GameObject bullet;

public float shootForce, upwardForce;

public float timeBetweenShooting, spread, reloadTime, timeBetweenShoots;
public int magazineSize, bulletsPerTap;
public bool allowButtonHold;

int bulletsLeft, bulletsShot;

bool shooting, readyToShoot, reloading;

public Transform attackpoint;

public bool allowInvoke = true;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void MyInput()
    {
        if(shooting = Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            Debug.Log("Fire Missile");
        }
        if(readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = 0;

            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        bulletsLeft--;
        bulletsShot++;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();
    }
}
