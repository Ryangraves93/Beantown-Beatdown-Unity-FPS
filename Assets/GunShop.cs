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
            purchaseText.GetComponent<TextMeshProUGUI>().text = "Purchase small gun - E"; 
            smallGun = true;
            Debug.Log(smallGun + "smallGun");
        }
        if (gameObject.gameObject.CompareTag("MediumGun"))
        {
            purchaseText.GetComponent<TextMeshProUGUI>().text = "Purchase Medium gun - E";
            mediumGun = true;
            Debug.Log(mediumGun + "mediumGun");
        }
        if (gameObject.gameObject.CompareTag("HeavyGun"))
        {
            purchaseText.GetComponent<TextMeshProUGUI>().text = "Purchase Heavy gun - E";
            heavyGun = true;
            Debug.Log(heavyGun + "heavyGun");
        }

    }
    private void OnTriggerExit(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            purchaseText.SetActive(false);
            playerInRange = false;
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
        if (smallGun == true)
        {
            gunController.smallGunPurchase = true;
        }
        if (mediumGun == true)
        {
            {
                Debug.Log("Purchased medium");
                gunController.mediumGunPurchased = true;
            }
        }

        if (heavyGun == true)
            {
                Debug.Log("Purchased heavy");
                gunController.heavyGunPurchased = true;
            }
            purchased = true;
            purchaseText.SetActive(false);
            GameObject.Destroy(gun);
            LivingEntity.score -= gunValue;
        }
    
}
