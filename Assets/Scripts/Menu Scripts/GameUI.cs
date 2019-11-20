using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameUI : MonoBehaviour
{
    public GameObject player;
    public Image FadePlan;
    public GameObject gameOverUI;
    public TextMeshProUGUI playerHealth;
    public TextMeshProUGUI playerAmmo;

    public GunScript gunScript;


    bool dead = false;
   
    // Start is called before the first frame update
    void Start()
    {
        gunScript = GetComponent<GunScript>();
        FindObjectOfType<Player>().OnDeath += OnGameOver;
    }

    private void Update()
    {
        if (dead == true)
        {
    
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartNewGame();
            }  
        }
        playerUI();

    }
    void OnGameOver()
    {
        dead = true;
        StartCoroutine(Fade(Color.clear, Color.black, 1));
        gameOverUI.SetActive(true);
    }

    IEnumerator Fade (Color from, Color to, float time)
    {
        float speed = 1 / time;
        float percent = 0;

        while (percent < 1)
        {
            percent += Time.deltaTime * speed;
            FadePlan.color = Color.Lerp(from, to, percent);
          
            yield return null;

        }

    }
    public void playerUI()
    {
        if (dead == true)
        {
            playerHealth.text = "";
        }
        playerHealth.text = "Health - " + LivingEntity.FindObjectOfType<Player>().health.ToString();
        playerAmmo.text = "Ammo" + " - " + GunScript.FindObjectOfType<GunScript>().currentAmmo.ToString() + " / " + GunScript.FindObjectOfType<GunScript>().maxAmmo.ToString(); 
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("Main Level");
    }
}
