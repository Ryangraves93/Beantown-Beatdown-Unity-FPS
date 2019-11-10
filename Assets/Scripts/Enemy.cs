using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 1;
    public Transform character;
    public NavMeshAgent agent;

    
    
    public ParticleSystem deathEffect;

    private void Start()
    {
      
        agent = GetComponent<NavMeshAgent>();
        var mask = LayerMask.GetMask(new[] { "Actors" });
        
        Debug.Log("Agent destination is " + agent.destination);
    }

    private void FixedUpdate()
    {
        //agent.destination = character.position;
        agent.SetDestination(character.position);
    }

    public void TakeDamage(float amount, Vector3 enemyPos, Vector3 hitDirection)
    {
        //rb.AddForceAtPosition(Vector3.forward * 10, hitDirection);
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
