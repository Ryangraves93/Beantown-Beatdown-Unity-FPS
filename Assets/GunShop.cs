using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GunShop : MonoBehaviour
{
    public GameObject purchaseText;
    public GameObject gun;
    public GunScript gunScript;
    bool purchase = false;
    bool purchased = false;
    bool playerInRange = false;

    bool smallGun = false;
    bool mediumGun = false;
    bool heavyGun = false;
    public LayerMask LayerMask;

    public LivingEntity player;
    public int gunValue;
    public GunController gunController;

    void Start()
    {
        gunController = FindObjectOfType<GunController>();
        gunScript = FindObjectOfType<GunScript>();

      

   
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player") && purchased == false)
        {
            purchaseText.SetActive(true);
            playerInRange = true;

        }
        if (gameObject.gameObject.CompareTag("SmallGun"))
        {
            purchaseText.GetComponent<TextMeshProUGUI>().text = "Purchase small gun - E" + " " + gunValue; 
            smallGun = true;
   
        }
        if (gameObject.gameObject.CompareTag("MediumGun"))
        {
            purchaseText.GetComponent<TextMeshProUGUI>().text = "Purchase Medium gun -" + " " + gunValue;
            mediumGun = true;
      
        }
        if (gameObject.gameObject.CompareTag("HeavyGun"))
        {
            purchaseText.GetComponent<TextMeshProUGUI>().text = "Purchase Heavy gun - " + " " + gunValue;
            heavyGun = true;
        
        }

    }
    private void OnTriggerExit(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            purchaseText.SetActive(false);
            playerInRange = false;
            smallGun = false;
            mediumGun = false;
            heavyGun = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E) && LivingEntity.score >= gunValue && playerInRange == true)
        {
            purchaseWeapon();
        }
    }

    void purchaseWeapon()
    {
        gunController.gunToBeEquipped = false;
        Debug.Log(gunController.gunToBeEquipped + "Guntobequ");
        if (smallGun == true)
        {
           
            gunController.smallGunPurchased = true;
        }
        if (mediumGun == true)
        {
            {
     
                gunController.mediumGunPurchased = true;
                
            }
        }

        if (heavyGun == true)
            {
            
          
                gunController.heavyGunPurchased = true;
            }
            purchased = true;
            purchaseText.SetActive(false);
            GameObject.Destroy(gun);
            LivingEntity.score -= gunValue;
           
    }
    
}
