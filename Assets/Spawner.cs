using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    Vector3 randomPositon;
    void Start()
    {
        Vector3 randomPositon = new Vector3(Random.Range(0f, 10.0f), 0, Random.Range(0f, 10.0f));
    }

    public void update()
    { 
        ObjectPooler.Instance.SpawnFromPool("Enemy", randomPositon, Quaternion.identity);
        Debug.Log("Spawned");
    }
}
