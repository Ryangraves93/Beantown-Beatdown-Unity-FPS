using System.Collections;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    //Bullet Variables
    public Transform muzzle;
    public Projectile projectile;
    public float msBetweenShots = 100;
    public float muzzleVelocity = 35;
    float nextShotTime;

    //Ammo and Reloading Variables
    bool isReloading = false;
    public int maxAmmo = 6;
    public int currentAmmo;
    public float reloadTime = 1f;
    
    //Recoil Variable
    Vector3 recoilSmoothDampVelocity;


    public void Start()
    {
        currentAmmo = maxAmmo;//Set's maxAmmo on start
    }
    public void Update()
    {
        //Debug.Log(hasBeenInstansiated);
        if (currentAmmo <= 0)
        {
           StartCoroutine(Reload());
            return;
        }
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, Vector3.zero, ref recoilSmoothDampVelocity, .1f);
    }
    //Shoot function instantiates a projectile at the muzzle positioni and uses the SetSpeed function to determine the speed of the projectile
    public void Shoot()
    {
        if (isReloading)
            return;
        if (Time.time > nextShotTime)//Sets the time between shots and instantiates projects at the muzzle location
        {
            nextShotTime = Time.time + msBetweenShots / 1000;
            Projectile newProjectile = Instantiate(projectile, muzzle.position, muzzle.rotation) as Projectile;
            newProjectile.SetSpeed(muzzleVelocity);
            transform.localPosition -= Vector3.forward * .2f;
            currentAmmo--;
        }
        
    }
   
     IEnumerator Reload() //Passes in the reload time as the waitForSeconds in the Enumerator which halts a set time for player reloading
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
    }
    
}
