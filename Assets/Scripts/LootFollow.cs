using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootFollow : MonoBehaviour
{
    public Transform target;
    public float minModifier = 7;
    public float maxModifier = 11;

    bool isFollowing = false;

    Vector3 _velocity = Vector3.zero;
    void Start()
    {
        
    }

    public void StartFollowing()
    {
        isFollowing = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (isFollowing)
        {
            transform.position = Vector3.SmoothDamp(transform.position, target.position, ref _velocity, Time.deltaTime * Random.Range(minModifier, maxModifier));
        }
    }
}
