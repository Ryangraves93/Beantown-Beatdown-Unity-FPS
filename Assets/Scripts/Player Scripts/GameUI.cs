using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameUI : MonoBehaviour
{
    //Reference variables for GameObjects
    public GameObject player;
    public Image FadePlan;
    public GameObject gameOverUI;
    public GunScript gunScript;

    
    //Variables for HUD
    
    public TextMeshProUGUI playerHealth;
    public TextMeshProUGUI playerAmmo;
    public TextMeshProUGUI playerScore;
    
    bool dead = false;
   
    // Start is called before the first frame update
    void Start()
    {
        gunScript = GetComponent<GunScript>();
    
        FindObjectOfType<Player>().OnDeath += OnGameOver;
    }

    private void Update()
    {
        if (dead == true )
        {   
            if (Input.GetKeyDown(KeyCode.Space))//Responsible for resseting the game once the player has died
            {
                StartNewGame();
            }  
        }
        playerUI();     
    }
    //Set's the gameOverUi to true on death and starts the Courotine for the fade gameOver scene
    void OnGameOver()
    {
        dead = true;
        StartCoroutine(Fade(Color.clear, Color.black, 1));
        gameOverUI.SetActive(true);
    }
    //Function for creating a fade effect when the player dies
    public IEnumerator Fade (Color from, Color to, float time)
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
    //PlayerUI set's the information for the HUD. It set's the player health,ammo and score
    public void playerUI()
    {
        if (dead == true)
        {
            playerHealth.gameObject.SetActive(false);
            playerAmmo.gameObject.SetActive(false);
            playerScore.gameObject.SetActive(false);
        }
        playerHealth.text = "Health - "  + LivingEntity.FindObjectOfType<Player>().health.ToString() + " / " +LivingEntity.FindObjectOfType<Player>().startingHealth.ToString();
        if (GunScript.FindObjectOfType<GunScript>() != null)
        {
            playerAmmo.text = "Ammo" + " - " + GunScript.FindObjectOfType<GunScript>().currentAmmo.ToString() + " / " + GunScript.FindObjectOfType<GunScript>().maxAmmo.ToString();
        }
            playerScore.text = "Beans" + " - " + LivingEntity.score.ToString();
        
    }

    public void StartNewGame()//Loads Main level scene
    {
        LivingEntity.score = 10;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
