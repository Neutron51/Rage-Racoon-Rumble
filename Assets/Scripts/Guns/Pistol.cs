using UnityEngine;
using TMPro;

public class Pistol : MonoBehaviour
{
    public int damage;
    public Transform FirePoint;
    public GameObject Fire;
    public GameObject HitPoint;
    private Animator animator;
    
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;
    bool shooting, readyToShoot, reloading;

    public TextMeshProUGUI text;

    /*public AudioClip ShootSFX;
    public AudioClip EmptyClip;
    public AudioSource source;*/

    private void Awake()
    {
        animator = GetComponent<Animator>();
        /*source = GetComponent<AudioSource>();
        EmptyClip = GetComponent<AudioClip>();*/
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    void Update()
    {
        MyInput();

        /*if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            rpg.IsFiring = true;
        }

        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            rpg.IsFiring = false;
        }*/
        text.SetText(bulletsLeft + " / " + magazineSize);
    }

    private void MyInput()
    {
        if(allowButtonHold)
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
            Debug.Log("Shoot!");
        }

        if(Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
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
            //animator.SetBool("IsShooting", true);
            animator.SetTrigger("Shoot");
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
        /*else
        {
            animator.SetBool("IsShooting", false);
        }*/
    }

    void Shooting()
    {
        RaycastHit hit;
        readyToShoot = false;

        if(Physics.Raycast(FirePoint.position, transform.TransformDirection(Vector3.forward), out hit, 100))
        {
            Debug.DrawRay(FirePoint.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.orange);

            //GameObject a = Instantiate(Fire, FirePoint.position, Quaternion.identity);
            //GameObject b = Instantiate(HitPoint, hit.point, Quaternion.identity);

            //Destroy(a, 1);
            //Destroy(b, 1);

            if(hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<Enemy>().Damage(damage);
            }
        }

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);
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
