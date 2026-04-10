using UnityEngine;
using UnityEngine.Video;
using TMPro;

public class UZIShoot : MonoBehaviour
{
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    bool shooting, readyToShoot, reloading;

    public Transform shootingPos;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    //public CamShake camShake;
    public TextMeshProUGUI text;

    private void Start()
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
        if(allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if(Input.GetKeyDown(KeyCode.R)&& bulletsLeft < magazineSize && !reloading) Reload();

        //Shoot
        if(readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        //Spread
        //float x = Random.Range(-spread, spread);
        //float y = Random.Range(-spread, spread);

        //Calculating direction with Spread
        //Vector3 direction = shootingPos.transform.TransformDirection(Vector3.forward) + new Vector3(x, y, 0);

        //RayCast
        if(Physics.Raycast(shootingPos.transform.position, transform.TransformDirection(Vector3.forward), out rayHit, range, whatIsEnemy))
        {
            Debug.Log(rayHit.collider.name);
            if(rayHit.collider.CompareTag("Enemy"))
                rayHit.collider.GetComponent<Enemy>().Damage(damage);        
                Debug.DrawRay(shootingPos.position, transform.TransformDirection(Vector3.forward) * rayHit.distance, Color.orange);        
        }

        bulletsLeft--;
        Invoke("ResetShot", timeBetweenShooting);
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadingFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
