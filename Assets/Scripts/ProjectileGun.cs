using UnityEngine;

public class ProjectileGun : MonoBehaviour
{
    public GameObject bullet;

    public float shootForce, upwardForce;

    public float timeBetweenShooting, spread, reloadTime, timeDetweenShots;
    public int magazineSize, bulletsSpread;
    public bool allowButtonHold;

    int bulletsLeft, bulletsShot;

    bool shooting, readuToShoot, reloading;

    public Camera fpsCam;
    public Transform attackPoint;

    public bool allowInvoke = true;
}
