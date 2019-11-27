using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    bool inRange = false;
    public GameObject purchaseText;
    int healthPrice = 5;
    float maxPlayerHealth = 5;

 
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inRange == true)
            {
            if (LivingEntity.FindObjectOfType<Player>().health < maxPlayerHealth)
            {
                LivingEntity.FindObjectOfType<Player>().health += 1;
                LivingEntity.score -= 5;
            }
            }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            purchaseText.gameObject.SetActive(true);
            purchaseText.GetComponent<TextMeshProUGUI>().text = "Purchase Health - "  + healthPrice;
            inRange = true;

        }
    }
    public void OnTriggerExit(Collider other)
    {
        purchaseText.gameObject.SetActive(false);
        inRange = false;
    }
}
