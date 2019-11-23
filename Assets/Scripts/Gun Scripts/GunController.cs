using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform weaponHold;
    public GunScript smallGun;
    public GunScript mediumGun;
    public GunScript heavyGun;
    //public GunScript equippedGun;
    public GunScript equippedGun;

    public bool smallGunPurchased = false;
    public bool mediumGunPurchased = false;
    public bool heavyGunPurchased = false;
    bool MediumGunEquipped = true;
    bool HeavyGunEquipped = true;

    public bool gunToBeEquipped;
    
    private void Start()
    {
        //EquipGun(equippedGun);
        
        InstansiateGuns();
        


    }
    //Calls three if statements to determine which gun for the player to equip
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1) && smallGunPurchased == true || (smallGunPurchased == true && gunToBeEquipped == false))
        {
            //EquipGun(smallGun);
            if (equippedGun != null)
            {
                equippedGun.gameObject.SetActive(false);
            }
            smallGun.gameObject.SetActive(true);
            equippedGun = smallGun;
            gunToBeEquipped = true;
            Debug.Log(gunToBeEquipped);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && mediumGunPurchased == true || (mediumGunPurchased == true && gunToBeEquipped == false))
        {
            if (equippedGun != null)
            {
                equippedGun.gameObject.SetActive(false);
            }
            mediumGun.gameObject.SetActive(true);
            equippedGun = mediumGun;
            gunToBeEquipped = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && heavyGunPurchased == true || (heavyGunPurchased == true && gunToBeEquipped == false))
        {
            if (equippedGun != null)
            {
                equippedGun.gameObject.SetActive(false);
            }
            heavyGun.gameObject.SetActive(true);
            equippedGun = heavyGun;
            gunToBeEquipped = true;
        }
    }
    //Function used to equip a weapon passed in which uses the GunScript class. Instansiates it at the weaponHold GameObject
    /*public void EquipGun(GunScript gunToEquip)
    {

        Debug.Log(gunToEquip.GetComponent<GunScript>().hasBeenInstansiated + "meme");
        if (gunToEquip.GetComponent<GunScript>().hasBeenInstansiated == false)
        {
            equippedGun = Instantiate(gunToEquip, weaponHold.position, weaponHold.rotation) as GunScript;
            equippedGun.transform.parent = weaponHold;
            gunToEquip.GetComponent<GunScript>().hasBeenInstansiated = true;
            Debug.Log(gunToEquip.hasBeenInstansiated + "Check 1");

        }
            if (gunToEquip.GetComponent<GunScript>().hasBeenInstansiated == true)
            {
                if (equippedGun != null)
                {
                    equippedGun.gameObject.SetActive(false);
                }
                gunToEquip.gameObject.SetActive(true);
                Debug.Log(gunToEquip.hasBeenInstansiated + "Check 2");

            }
   
    }*/

    void InstansiateGuns()
    {
        smallGun = Instantiate(smallGun, weaponHold.position, weaponHold.rotation) as GunScript;
        smallGun.transform.parent = weaponHold;
        smallGun.gameObject.SetActive(false);
        mediumGun = Instantiate(mediumGun, weaponHold.position, weaponHold.rotation) as GunScript;
        mediumGun.transform.parent = weaponHold;
        mediumGun.gameObject.SetActive(false);
        heavyGun = Instantiate(heavyGun, weaponHold.position, weaponHold.rotation) as GunScript;
        heavyGun.transform.parent = weaponHold;
        heavyGun.gameObject.SetActive(false);
    }
            
      public void Shoot()//Calls the shoot function
    {
        if (equippedGun != null)
        {
            equippedGun.Shoot();
        }
    }
}
