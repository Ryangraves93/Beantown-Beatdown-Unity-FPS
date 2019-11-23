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
    Text waveText;

    public Transform spawnPosition;
    public Transform spawnPosition2;
    Vector3 spawnerLocation;

    Wave currentWave;
    int currentWaveNumber;

    int enemiesRemainingToSpawn;
    int enemiesRemainingAlive;
    float nextSpawnTime;

    bool spawner1 = true;
    bool startGameText = false;

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
       
        if (startGameText == true && playerGunController.gunToBeEquipped == true)
        {
            StartCoroutine(timeBetweenRounds());
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
            NextWave();
        }
    }

    void NextWave()
    {
        startGameText = false;
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
    }
    IEnumerator StartGameText()
    {
        waveUI.text = "Defend Beantown from waves of enemies";
        yield return new WaitForSeconds(2f);
        waveUI.text = "Keep out of range of the enemies";
        yield return new WaitForSeconds(2f);
        waveUI.text = "Buy a pistol from the shop and upgrade between rounds!";
        yield return new WaitForSeconds(2f);
        
        waveUI.text = "";
       startGameText = true;
    }

    IEnumerator timeBetweenRounds()
    {
        waveUI.text = "10 seconds before the next round!";
        yield return new WaitForSeconds(10f);
    }
    IEnumerator WaveUI(int waveNumber)
    {
        waveNumber = waveNumber + 1;
        waveUI.text = "Wave Number - " + waveNumber.ToString();
        yield return new WaitForSeconds(2f);
        waveUI.text = "";
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
