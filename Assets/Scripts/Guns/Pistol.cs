using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;
using System.Collections;

public class Pistol : MonoBehaviour
{
    [SerializeField]
    private TrailRenderer BulletTrail;

    public int damage;
    public Transform FirePoint;
    public GameObject Fire;
    public GameObject HitPoint;
    public Animator animator;
    
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;
    bool shooting, readyToShoot, reloading;

    public TextMeshProUGUI text;

    public AudioSource ShootSFX;
    //public AudioSource EmptySFX;
    public AudioSource ReloadSFX;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
        ShootSFX = GetComponent<AudioSource>();
        //EmptySFX = GetComponent<AudioSource>();
        ReloadSFX = GetComponent<AudioSource>();
    }

    void Update()
    {
        MyInput();
        text.SetText(bulletsLeft + " / " + magazineSize);
    }

    private void OnDisable()
    {
        shooting = false;
    }

    private void MyInput()
    {
        if(allowButtonHold)
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
            Debug.Log("Shoot!");
        }

        if(Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading  && this.gameObject.activeSelf)
        {
            Reload();
        }

        //Shoot
        if(readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shooting();
            Debug.Log("Shooting");
        } 

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetBool("IsShooting", true);
        }
        else
        {
            animator.SetBool("IsShooting", false);
        }
    }

    void Shooting()
    {
        readyToShoot = false;

        if(Physics.Raycast(FirePoint.position, (FirePoint.forward) * 2f, out RaycastHit hit, 100))
        {
            TrailRenderer trail = Instantiate(BulletTrail, FirePoint.position, Quaternion.identity);

            StartCoroutine(SpawnTrail(trail, hit));

            /*if(hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<Enemy>().Damage(damage);
            }*/
            //bulletTracer.SetPosition(1, hit.point);    
            //GameObject a = Instantiate(Fire, FirePoint.position, Quaternion.identity);
            //GameObject b = Instantiate(HitPoint, hit.point, Quaternion.identity);

            //Destroy(a, 1);
            //Destroy(b, 1);

            /*if(hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<Enemy>().Damage(damage);
            }*/
        }
        /*else
        {
            bulletTracer.SetPosition(1, FirePoint.position + transform.TransformDirection(Vector3.right) * range);
        }*/

        BulletDamage();
        
        ShootSFX.Play();

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit hit)
    {
        float time = 0;
        Vector3 startPos = trail.transform.position;

        while(time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPos, hit.point, time);
            time += Time.deltaTime / trail.time;

            yield return null;
        }

        trail.transform.position = hit.point;

        Destroy(trail.gameObject, trail.time);
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void Reload()
    {
        reloading = true;
        ReloadSFX.Play();
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }

    private void BulletDamage()
    {
        if(Physics.Raycast(FirePoint.position, transform.TransformDirection(Vector3.right), out RaycastHit hit, 100))
        {
            if(hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<EnemyController>().TakenDamage(10);
                Debug.Log("Enemy hit!");
            }
        }
    }
}
