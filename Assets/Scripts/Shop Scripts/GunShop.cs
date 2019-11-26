using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GunShop : MonoBehaviour
{
    //Reference variables for guns
    public GameObject purchaseText;
    public GameObject gun;
    public GunScript gunScript;
    public LivingEntity player;

    //Bool variables to determine whether a player has purchased the item 
    bool purchase = false;
    bool purchased = false;
   
    //Bool variables for which gun collider the player is in
    bool smallGun = false;
    bool mediumGun = false;
    bool heavyGun = false;
    bool playerInRange = false;


    public int gunValue;
    public GunController gunController;

    void Start()//Gets reference to scripts on objects
    {
        gunController = FindObjectOfType<GunController>();
        gunScript = FindObjectOfType<GunScript>();
    }

    private void OnTriggerEnter(Collider c)//Determines which collider the player is in and acts accordingly
    {
        if (c.CompareTag("Player") && purchased == false)
        {
            purchaseText.SetActive(true);
            playerInRange = true;
        }
        if (gameObject.gameObject.CompareTag("SmallGun"))
        {
            purchaseText.GetComponent<TextMeshProUGUI>().text = "Purchase small gun - " + " " + gunValue; 
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
    private void OnTriggerExit(Collider c)//Sets booleans to false on exit of colliders
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
        //Checks if the player has enough score to purchase and if he is within the collider
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
        //Sets the gun to purchased so it cannot be purchased again, destroys the game object and takes away the score
        purchased = true;
        purchaseText.SetActive(false);
        GameObject.Destroy(gun);
        LivingEntity.score -= gunValue;
           
    }
    
}
