using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform weaponHold;
    public GunScript smallGun;
    public GunScript mediumGun;
    public GunScript heavyGun;
    GunScript equippedGun;

    private void Start()
    {
        if (smallGun != null)
        {
            EquipGun(smallGun);
        }

    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Hello");
            EquipGun(smallGun);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Hello");
            EquipGun(mediumGun);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("Hello");
            EquipGun(heavyGun);
        }
    }
    public void EquipGun(GunScript gunToEquip)
    {
        if (equippedGun != null)
        {
            Destroy(equippedGun.gameObject);
        }
        equippedGun = Instantiate(gunToEquip, weaponHold.position,weaponHold.rotation) as GunScript;
        equippedGun.transform.parent = weaponHold;
    }

    public void Shoot()
    {
        if (equippedGun != null)
        {
            equippedGun.Shoot();
        }
    }
}
