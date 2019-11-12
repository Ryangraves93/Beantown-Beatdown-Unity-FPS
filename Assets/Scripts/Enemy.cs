using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

[RequireComponent (typeof (NavMeshAgent))]
public class Enemy : LivingEntity
{
    public enum State { Idle, Chasing, Attacking};
    State currentState;

    //public float health = 1;
    Transform target;
    LivingEntity targetEntitiy;
    public NavMeshAgent agent;

    float attackDistanceThreshold = .5f;
    float timeBetweenAttacks = 1;
    float damage = 1;

    float nextAttackTime;
    float enemyCollisionRadius;
    float targetCollisionRadius;

    bool hasTarget;

    public ParticleSystem deathEffect;

    protected override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();

        if (GameObject.FindGameObjectWithTag("Player") != null) 
        { 
            currentState = State.Chasing;
            hasTarget = true;
            target = GameObject.FindGameObjectWithTag("Player").transform;
            targetEntitiy = target.GetComponent<LivingEntity>();
            targetEntitiy.OnDeath += OnTargetDeath;

            enemyCollisionRadius = GetComponent<CapsuleCollider>().radius;
            targetCollisionRadius = target.GetComponent<CapsuleCollider>().radius;
            StartCoroutine(UpdatePath());
        }
    }

    void OnTargetDeath()
    {
        hasTarget = false;
        currentState = State.Idle;
    }

    private void FixedUpdate()
    {
        if (hasTarget) { 
            if (Time.time > nextAttackTime)
            {
                float sqrDistance = (target.position - transform.position).sqrMagnitude;

                if (sqrDistance < Mathf.Pow(attackDistanceThreshold + enemyCollisionRadius + targetCollisionRadius, 2))
                {
                    nextAttackTime = Time.time + timeBetweenAttacks;
                    StartCoroutine(Attack());
                }
            }
        }  
    }

    IEnumerator Attack()
    {
        currentState = State.Attacking;
        agent.enabled = false;

        Vector3 originalposition = transform.position;
        Vector3 dirToTarget = (target.position - transform.position).normalized;
        Vector3 attackPosition = target.position - dirToTarget * (enemyCollisionRadius);
    

        float attackSpeed = 3;
        float percent = 0;

        bool hasAppliedDamage = false;

        while (percent <= 1)
        {

            if (percent >= .5f && !hasAppliedDamage)
            {
                hasAppliedDamage = true;
                targetEntitiy.TakeDamage(damage);
            }
            percent += Time.deltaTime * attackSpeed;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector3.Lerp(originalposition, attackPosition, interpolation);
            yield return null;
        }
        currentState = State.Chasing;
        agent.enabled = true;
    }
    IEnumerator UpdatePath()
    {
        float refreshRate = .50f;
        while (hasTarget)
        {
            if (currentState == State.Chasing)
            {
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                Vector3 targetPosition = target.position - dirToTarget * (enemyCollisionRadius + targetCollisionRadius + attackDistanceThreshold/2);
                if (!dead)
                {
                    agent.SetDestination(targetPosition);
                }
            }
            yield return new WaitForSeconds(refreshRate);
        }
        
    }


}
