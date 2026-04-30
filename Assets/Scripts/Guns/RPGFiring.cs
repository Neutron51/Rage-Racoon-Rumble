using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using UnityEngine;
using TMPro;

public class RPGFiring : MonoBehaviour
{
    public GameObject rocketPrefab;

    public float shootForce, upwardForce;

    public float timeBetweenShooting, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;
    bool shooting, readyToShoot, reloading;

    public Camera fpsCam;
    public Transform attackPoint;

    public bool allowInvoke = true;

    public TextMeshProUGUI text;

    public AudioSource ShootSFX;

    void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
        ShootSFX = GetComponent<AudioSource>();
    }

    void Update()
    {
        MyInput();
        text.SetText(bulletsLeft + " / " + magazineSize);
    }

    private void MyInput()
    {
        if (allowButtonHold)
        {
            shooting = Input.GetKey(KeyCode.Mouse0);
        }
        else
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
        {
            Reload();
        }

        //Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = 0;
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        //Find the exact hit position using a raycast
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(75); // if it doesn't hit anything, set the target point to 75 units in front of the camera
        }
        
        Vector3 directionWithoutSpread = (targetPoint - attackPoint.position) * -1;

        //Instantiate the rocket
        GameObject currentRocket = Instantiate(rocketPrefab, attackPoint.position, Quaternion.identity);

        currentRocket.transform.forward = directionWithoutSpread.normalized;

        //Add forces to the rocket
        currentRocket.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * shootForce, ForceMode.Impulse);

        bulletsLeft--;
        bulletsShot++;

        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }

        if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }

            bulletsLeft--;
            bulletsLeft++;

        if(allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }

        ShootSFX.Play();

        Destroy(currentRocket, 3f);
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
