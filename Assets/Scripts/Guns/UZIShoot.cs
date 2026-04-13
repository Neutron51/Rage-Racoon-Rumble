using UnityEngine;
using UnityEngine.Video;
using TMPro;
using UnityEditor.Rendering.Universal;

public class UZIShoot : MonoBehaviour
{
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    bool shooting, readyToShoot, reloading;

    public Camera tdsCam;
    public Transform shootingPos;
    public LayerMask whatIsEnemy;

    //public CamShake camShake;
    public TextMeshProUGUI text;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Update()
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

        if(Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
        {
            Reload();
        }

        //Shoot
        if(readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
            Debug.Log("Shooting!");
        } 
    }

    private void Shoot()
    {
        RaycastHit rayHit;
        readyToShoot = false;

        //Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculating direction with Spread
        //Vector3 direction = tdsCam.transform.forward + new Vector3(x, y, 0);

        //RayCast
        if(Physics.Raycast(shootingPos.position, transform.TransformDirection(Vector3.forward), out rayHit, range, whatIsEnemy))
        {
            Debug.DrawRay(shootingPos.position, transform.TransformDirection(Vector3.forward) * rayHit.distance, Color.orange);
            //Debug.Log(rayHit.collider.name);
            if(rayHit.collider.CompareTag("Enemy"))
            {
                rayHit.collider.GetComponent<Enemy>().Damage(damage);
            }
        }

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        //if(bulletsLeft > 0 && bulletsLeft > 0)
        //Invoke("Shoot", timeBetweenShots);
    }

    private void ResetShot()
    {
        readyToShoot = true;
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
