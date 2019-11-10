using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemy;
    public Transform player;
    public int enemyCount;

    public float yPos = 0;
   
    void Start()
    {
        Debug.Log("This one" + enemy.transform.position);
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
    }

}
