using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemySpawner : MonoBehaviour
{

    public Wave[] waves;
    public Enemy enemy;

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
            spawnedEnemy.SetChracteristics(currentWave.hitsToKillPlayer, currentWave.enemyHealth);
        }
    }

    void OnEnemyDeath()
    {
        enemiesRemainingAlive--;

        if (enemiesRemainingAlive == 0)
        {
            StartCoroutine(timeBetweenRounds());
            Debug.Log("Cunttttt");
            
        }
        

        
    }

    void NextWave()
    {
        
        startGameTextHasBeenPlayed = false;
        if (firstRoundFinished == true)
        {
           
        }
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
        waveUI.text = "Keep out of range of the enemies to avoid attacks";
        yield return new WaitForSeconds(3f);
        waveUI.text = "Buy a pistol from the shop with your beans";
        yield return new WaitForSeconds(3f);

        waveUI.gameObject.SetActive(false);
        startGameTextHasBeenPlayed = true;
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
        waveUI.text = "Wave Number - " + waveNumber.ToString();
        yield return new WaitForSeconds(2f);
        waveUI.gameObject.SetActive(false);
    }

    [System.Serializable]
    public class Wave
    {
        public int enemyCount;
        public float timeBetweenSpawns;
        public int hitsToKillPlayer;
        public float enemyHealth;

    }
    

}
