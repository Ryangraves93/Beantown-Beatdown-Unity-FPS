using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public Camera FpsCam;
    public ParticleSystem muzzleFlash;
    private float nextTimeToFire = 0f;

    public Animator animator;

    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;

    private void Start()
    {
        currentAmmo = maxAmmo;
    }

    private void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }
    void Update()
    {

        if (isReloading)
            return;
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            muzzleFlash.Play();
            Shoot();
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime);
        animator.SetBool("Reloading", false);

        currentAmmo = maxAmmo;
        isReloading = false;
    }
    void Shoot()
    {
        currentAmmo--;
        RaycastHit hit;
        if (Physics.Raycast(FpsCam.transform.position, FpsCam.transform.forward, out hit, range))//Sends out a raycast from the player
        {
            
            Vector3 enemyPos = hit.transform.position;
            Debug.Log(enemyPos);
            Enemy enemy = hit.transform.GetComponent<Enemy>();//Assigns hit information from raycast to the variable enemy
            if (enemy != null)
            {
                enemy.TakeDamage(damage,enemyPos,transform.forward);//Passes in the damage variable into the enemy class
            }
        }
    }
}
