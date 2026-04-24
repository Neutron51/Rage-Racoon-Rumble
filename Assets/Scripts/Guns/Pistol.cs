using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;
using System.Collections;

public class Pistol : MonoBehaviour
{
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

    //private LineRenderer bulletTracer;

    public TextMeshProUGUI text;

    /*public AudioClip ShootSFX;
    public AudioClip EmptyClip;
    public AudioSource source;*/

    private void Awake()
    {
        //bulletTracer = GetComponent<LineRenderer>();
        /*source = GetComponent<AudioSource>();
        EmptyClip = GetComponent<AudioClip>();*/
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    void Update()
    {
        MyInput();
        text.SetText(bulletsLeft + " / " + magazineSize);
        //StartCoroutine(ShotEffects());

        /*if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            rpg.IsFiring = true;
        }

        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            rpg.IsFiring = false;
        }*/
        
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
            /*if(bulletsLeft > 1)
            {
                source.(EmptyClip);
            }*/
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
            //animator.SetTrigger("Shoot");
            /*if(bulletsLeft > 1)
            {
                source.PlayOneShot(PistolShoot);
                return;
            }
            
            if(bulletsLeft == 0)
            {
                source.PlayOneShot(EmptyClip);
            }*/
        }
        else
        {
            animator.SetBool("IsShooting", false);
        }
    }

    void Shooting()
    {
        RaycastHit hit;
        readyToShoot = false;

        if(Physics.Raycast(FirePoint.position, transform.TransformDirection(Vector3.right), out hit, 100))
        {
            Debug.DrawRay(FirePoint.position, transform.TransformDirection(Vector3.right) * hit.distance, Color.orange);
            //bulletTracer.SetPosition(1, hit.point);    
            //GameObject a = Instantiate(Fire, FirePoint.position, Quaternion.identity);
            //GameObject b = Instantiate(HitPoint, hit.point, Quaternion.identity);

            //Destroy(a, 1);
            //Destroy(b, 1);

            if(hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<Enemy>().Damage(damage);
            }
        }
        /*else
        {
            bulletTracer.SetPosition(1, FirePoint.position + transform.TransformDirection(Vector3.right) * range);
        }*/

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);
    }

    /*private IEnumerator ShotEffects()
    {
        bulletTracer.enabled = true;

        yield return new WaitForSeconds(0.02f);

        bulletTracer.enabled = false;
    }*/

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
