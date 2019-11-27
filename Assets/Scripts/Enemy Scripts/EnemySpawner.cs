using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    //Enemy variables
    public Wave[] waves;
    public Enemy enemy;
    //Audio Variables
    public AudioSource gameSound;

    public TextMeshProUGUI waveUI;
    public TextMeshProUGUI endRoundUI;
    Text waveText;

    public float timeBeforeRoundStarts = 10f;

    public Transform spawnPosition;
    public Transform spawnPosition2;
    Vector3 spawnerLocation;

    Wave currentWave;
    int currentWaveNumber;

    int enemiesRemainingToSpawn;
    int enemiesRemainingAlive;
    float nextSpawnTime;

    public bool firstRoundFinished = false;
    bool spawner1 = true;
    bool startGameTextHasBeenPlayed = false;
    public bool playerReady = false;
    

    public event System.Action<int> OnNewWave;

    public GunController playerGunController;

    private void Start()
    {
        playerGunController = FindObjectOfType<GunController>();
        waveText = waveUI.GetComponentInChildren<Text>();
        StartCoroutine(StartGameText());
       
       }
    
    private void Update()
    {
        if ((playerReady == true) || (startGameTextHasBeenPlayed == true && playerGunController.gunToBeEquipped == true))
        {
            NextWave();
        }
        if (enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime)
        {
            if (spawner1 == true)
            {
                spawnerLocation.Set(spawnPosition.position.x, spawnPosition.position.y, spawnPosition.position.z);
                spawner1 = false;
             }
            else 
            {
                spawnerLocation.Set(spawnPosition2.position.x, spawnPosition2.position.y, spawnPosition2.position.z);
                spawner1 = true;
            }
            spawnerLocation.x += Random.Range(-2, 2);
            enemiesRemainingToSpawn--;
            nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;
            Enemy spawnedEnemy = Instantiate(enemy,spawnerLocation, Quaternion.identity) as Enemy;
            spawnedEnemy.OnDeath += OnEnemyDeath;
            spawnedEnemy.SetChracteristics(currentWave.enemyHealth);
        }
    }

    void OnEnemyDeath()
    {
        enemiesRemainingAlive--;

        if (enemiesRemainingAlive == 0)
        {
            if (currentWaveNumber == waves.Length)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                LivingEntity.score = 0;
            }
            else
            {
                StartCoroutine(timeBetweenRounds());
            }
        }     
    }

   

    void NextWave()
    {
       
        startGameTextHasBeenPlayed = false;
        StartCoroutine(WaveUI(currentWaveNumber));
        currentWaveNumber++;

        if (currentWaveNumber - 1 < waves.Length)
        {
            currentWave = waves[currentWaveNumber - 1];

            enemiesRemainingToSpawn = currentWave.enemyCount;
            enemiesRemainingAlive = enemiesRemainingToSpawn;

            if (OnNewWave != null)
            {
                OnNewWave(currentWaveNumber);
            }
        }
      
       
        playerReady = false;
    }
    IEnumerator StartGameText()
    {
        waveUI.gameObject.SetActive(true);
        waveUI.text = "Defend Beantown from waves of enemies";
        yield return new WaitForSeconds(3f);
        waveUI.text = "Kill enemies to earn beans and upgrade!";
        yield return new WaitForSeconds(3f);
        waveUI.text = "Get a pistol from the shop and defend yourself";
        yield return new WaitForSeconds(3f);

        waveUI.gameObject.SetActive(false);
        startGameTextHasBeenPlayed = true;
        startAudio();
    }

    void startAudio()
    {
        if (startGameTextHasBeenPlayed == true)
        {
            gameSound.Play();
        }
    }
    IEnumerator timeBetweenRounds()
    {
        
        endRoundUI.gameObject.SetActive(true);
        endRoundUI.text = timeBeforeRoundStarts.ToString() + " seconds before the next round starts";
        yield return new WaitForSeconds(3f);
        endRoundUI.text = "Prepare!";
        yield return new WaitForSeconds(timeBeforeRoundStarts - 3);
        endRoundUI.gameObject.SetActive(false);

        playerReady = true;
    }
    IEnumerator WaveUI(int waveNumber)
    {
        waveUI.gameObject.SetActive(true);
        waveNumber = waveNumber + 1;
        if (currentWaveNumber == waves.Length - 1)
        {
            waveUI.text = "Final Wave";
        }
        else
        {
            waveUI.text = "Wave Number - " + waveNumber.ToString();
        }
        yield return new WaitForSeconds(2f);
        waveUI.gameObject.SetActive(false);
    }

    [System.Serializable]
    public class Wave
    {
        public int enemyCount;
        public float timeBetweenSpawns;
        //public int hitsToKillPlayer;
        public float enemyHealth;

    }
    

}
