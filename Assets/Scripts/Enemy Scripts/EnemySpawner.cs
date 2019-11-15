using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{

    public Wave[] waves;
    public Enemy enemy;

    public GameObject waveUI;
    Text waveText;

    public Transform spawnPosition;

    Wave currentWave;
    int currentWaveNumber;

    int enemiesRemainingToSpawn;
    int enemiesRemainingAlive;
    float nextSpawnTime;

   

    public event System.Action<int> OnNewWave;

    private void Start()
    {
        waveText = waveUI.GetComponentInChildren<Text>();
        NextWave();
    }
    private void Update()
    {
        if (enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime)
        {
            enemiesRemainingToSpawn--;
            nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;
            Enemy spawnedEnemy = Instantiate(enemy, new Vector3 (Random.Range(spawnPosition.position.x, spawnPosition.position.x + 5),0,spawnPosition.position.z), Quaternion.identity) as Enemy;
            spawnedEnemy.OnDeath += OnEnemyDeath;
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

    IEnumerator WaveUI(int waveNumber)
    {
        waveNumber = waveNumber + 1;
        waveText.text = "Wave Number - " + waveNumber.ToString();
        yield return new WaitForSeconds(2f);
        waveText.text = "";
    }

    [System.Serializable]
    public class Wave
    {
        public int enemyCount;
        public float timeBetweenSpawns;
    }
    

}
