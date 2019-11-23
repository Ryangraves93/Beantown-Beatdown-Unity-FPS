using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ShopUiAppear : MonoBehaviour
{

    public GameObject shopText;
    TextMeshProUGUI floatingText;
    private void OnTriggerEnter(Collider c)
    {
        
        if (c.CompareTag("Player"))
        {
            shopText.GetComponent<TextMeshPro>().text = "0 Score         60 Score         100 Score";
            Debug.Log("Collided");
            
        }
    }

    private void OnTriggerExit(Collider c)
    {
        Debug.Log("Left Coliider");
        if (c.CompareTag("Player"))
        {
            shopText.GetComponent<TextMeshPro>().text = "";
        }
    }
}
