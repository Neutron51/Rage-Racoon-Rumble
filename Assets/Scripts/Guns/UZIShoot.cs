using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class UZIShoot : MonoBehaviour
{
    [SerializeField]
    private TrailRenderer BulletTrail;

    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    bool shooting, readyToShoot, reloading;

    public Camera tdsCam;
    public Transform shootingPos;
    public LayerMask whatIsEnemy;

    public Animator animator;

    public TextMeshProUGUI text;

    public AudioSource ShootSFX;
    //public AudioClip EmptySFX;
    //public AudioClip ReloadSFX;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        ShootSFX = GetComponent<AudioSource>();
        readyToShoot = true;
    }

    private void Update()
    {
        MyInput();
        text.SetText(bulletsLeft + " / " + magazineSize);
        if(animator != null)
        {
            if(Input.GetKey(KeyCode.Mouse0))
            {
                animator.SetBool("IsShooting", true);
            }
            else
            {
                animator.SetBool("IsShooting", false);
            }
        }
    }

    private void OnDisable()
    {
        shooting = false;
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

        if(Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading  && this.gameObject.activeSelf)
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
        readyToShoot = false;

        //RayCast
        if(Physics.Raycast(shootingPos.position, (shootingPos.up) * (-1), out RaycastHit rayHit, range, whatIsEnemy))
        {
            TrailRenderer trail = Instantiate(BulletTrail, shootingPos.position, Quaternion.identity);

            StartCoroutine(SpawnTrail(trail, rayHit));

            /*if(rayHit.collider.CompareTag("Enemy"))
            {
                rayHit.collider.GetComponent<Enemy>().Damage(damage);
            }*/
        }

        BulletDamage();

        ShootSFX.Play();

        //AudioManager1.Instance.PlaySFX(ShootSFX, 0.25f);

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit rayHit)
    {
        float time = 0;
        Vector3 startPos = trail.transform.position;

        while(time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPos, rayHit.point, time);
            time += Time.deltaTime / trail.time;

            yield return null;
        }

        trail.transform.position = rayHit.point;

        Destroy(trail.gameObject, trail.time);
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

    private void BulletDamage()
    {
        if(Physics.Raycast(shootingPos.position, transform.TransformDirection(Vector3.forward), out RaycastHit rayHit, range, whatIsEnemy))
        {
            if(rayHit.collider.CompareTag("Enemy"))
            {
                rayHit.collider.GetComponent<EnemyController>().TakenDamage(25);
                Debug.Log("Enemy HITS!");
            }
        }
    }
}
