using UnityEngine;

public class GunController : MonoBehaviour
{
    //Reference variables for gun prefabs
    public Transform weaponHold;
    public GunScript smallGun;
    public GunScript mediumGun;
    public GunScript heavyGun;

    //Reference variable for the currently equipped gun
    public GunScript equippedGun;

    //Boolean Variables to determine if the player can equip a gun
    public bool smallGunPurchased = false;
    public bool mediumGunPurchased = false;
    public bool heavyGunPurchased = false;
   

    public bool gunToBeEquipped;
    
    private void Start()
    {
        InstansiateGuns();//Instansiates guns on load
    }
  
    private void Update()
    {
        //Three conditional statments are made to check if the player has actively decided to switch to the gun
        //OR if that player has purchased the gun will automatically equip it for them
        if (Input.GetKeyDown(KeyCode.Alpha1) && smallGunPurchased == true ||
           (smallGunPurchased == true && gunToBeEquipped == false))
        {
            if (equippedGun != null)
            {
                equippedGun.gameObject.SetActive(false);
            }
            smallGun.gameObject.SetActive(true);
            equippedGun = smallGun;
            gunToBeEquipped = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && mediumGunPurchased == true || 
           (mediumGunPurchased == true && gunToBeEquipped == false))
        {
            if (equippedGun != null)
            {
                equippedGun.gameObject.SetActive(false);
            }
            mediumGun.gameObject.SetActive(true);
            equippedGun = mediumGun;
            gunToBeEquipped = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && heavyGunPurchased == true ||
           (heavyGunPurchased == true && gunToBeEquipped == false))
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
  
    void InstansiateGuns()//Instansiates all guns and set's each to inactive
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
            
      public void Shoot()//Calls the shoot function when equipped is assigned
    {
        if (equippedGun != null)
        {
            equippedGun.Shoot();
        }
    }
}
