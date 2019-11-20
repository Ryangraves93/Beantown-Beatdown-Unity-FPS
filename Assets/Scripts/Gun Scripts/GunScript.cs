using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public Transform muzzle;
    public Projectile projectile;
    public float msBetweenShots = 100;
    public float muzzleVelocity = 35;

    float nextShotTime;

    bool isReloading = false;
    public int maxAmmo = 6;
    public int currentAmmo;
    public float reloadTime = 1f;

    Vector3 recoilSmoothDampVelocity;
    public void Start()
    {
        currentAmmo = maxAmmo;
    }

    public void Update()
    {
        if (currentAmmo <= 0)
        {
           StartCoroutine(Reload());
            return;
        }

        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, Vector3.zero, ref recoilSmoothDampVelocity, .1f);
    }

    public void Shoot()
    {
        if (isReloading)
            return;
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + msBetweenShots / 1000;
        
        Projectile newProjectile = Instantiate(projectile, muzzle.position, muzzle.rotation) as Projectile;
        newProjectile.SetSpeed(muzzleVelocity);
        transform.localPosition -= Vector3.forward * .2f;
        currentAmmo--;
        }
        
    }

     IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("reloaded");
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
    }
    
}
