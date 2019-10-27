using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject enemy;
    public int enemyCount;
    

    public int xPos;
    public int zPos;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemyDrop());
    }

    IEnumerator EnemyDrop()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Debug.Log("Cunt");
            xPos = Random.Range(1, 8);
            zPos = Random.Range(1, 8);
            Instantiate(enemy, new Vector3(xPos, 0, zPos), Quaternion.identity);
            yield return new WaitForSeconds(2f);
        }
    }

}
