using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{

    public Wave[] waves;
    public Enemy enemy;

    public Transform spawnPosition;

    Wave currentWave;
    int currentWaveNumber;

    int enemiesRemainingToSpawn;
    int enemiesRemainingAlive;
    float nextSpawnTime;

    public event System.Action<int> OnNewWave;

    private void Start()
    {
        NextWave();
    }
    private void Update()
    {
        if (enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime)
        {
            enemiesRemainingToSpawn--;
            nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;
            Enemy spawnedEnemy = Instantiate(enemy, spawnPosition.position, Quaternion.identity) as Enemy;
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

    [System.Serializable]
    public class Wave
    {
        public int enemyCount;
        public float timeBetweenSpawns;
    }
    
    
    
    
    
    
    
    /*public GameObject enemy;
    public Transform player;
    public int enemyCount;

    public float yPos = 0;
   
    void Start()
    {
        //Debug.Log("This one" + enemy.transform.position);
        StartCoroutine(EnemyDrop());
    }

    public void Update()
    {
        
    }

    IEnumerator EnemyDrop()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            float xPos = Random.Range(1, 20);
            float zPos = Random.Range(1, 20);
            Instantiate(enemy, new Vector3(xPos, 0, zPos), Quaternion.identity);
            //yPos = enemy.GetComponent<Transform>().position.y;
            yield return new WaitForSeconds(2f);
        }
    }*/

}
