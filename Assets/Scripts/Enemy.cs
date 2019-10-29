using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 50f;
    public GameObject character;
    public NavMeshAgent agent;

    public ParticleSystem deathEffect;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        var mask = LayerMask.GetMask(new[] { "Actors" });
        var maxDistance = Vector3.Distance(this.transform.position, character.transform.position);
        var directionVector = character.transform.position - this.transform.position;
        //Debug.Log(directionVector);

        if (Physics.Raycast(this.transform.position, directionVector, maxDistance, mask))
        {
            agent.destination = character.transform.position;
             Debug.Log("Working");
            
        }
    }

    public void TakeDamage(float amount, Vector3 enemyPos, Vector3 hitDirection)
    {
        health -= amount;
        if (health <= 0f)
        {
            Instantiate(deathEffect, enemyPos, Quaternion.FromToRotation(Vector3.forward, hitDirection));
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
