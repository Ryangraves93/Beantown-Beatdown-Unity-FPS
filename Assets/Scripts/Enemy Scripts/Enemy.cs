using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

[RequireComponent (typeof (NavMeshAgent))]//Requires nav mesh for enemy pathfinding
public class Enemy : LivingEntity
{
    //Enum's used to determine which state the enemy is in
    public enum State { Idle, Chasing, Attacking};
    State currentState;

    //Pathfinding variables
    Transform target;
    LivingEntity targetEntitiy;
    public NavMeshAgent agent;

    //Variables used for enemy attacks
    float attackDistanceThreshold = .5f;
    float timeBetweenAttacks = 1;
    float damage = 1;
    float nextAttackTime;
    float enemyCollisionRadius;
    float targetCollisionRadius;
    bool hasTarget;

    //Particle system which is played on death
    public ParticleSystem deathEffect;

    protected override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();

        if (GameObject.FindGameObjectWithTag("Player") != null)//Determines if the enemy has a target and set's the player as the target
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
    //Sets attributes for enemies spawned so each wave can have enemies with different attributes
    public void SetChracteristics(int hitsToKillPlayer, float enemyHealth)
    {
        if (hasTarget)
        {
            damage = hitsToKillPlayer;
        }
        startingHealth = enemyHealth;
    }
    //Determines the point of contact for a collision and applies the death effect if enemies health drops lower than 0
    public override void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        if (damage >= health)
        {
            Destroy(Instantiate(deathEffect.gameObject, hitPoint, Quaternion.FromToRotation(Vector3.forward, hitDirection)) as GameObject, deathEffect.startLifetime);  
        }
        base.TakeHit(damage, hitPoint, hitDirection);
    }
    //This function is used so when the player dies we don't get errors as the enemy attempts to locate the target
    void OnTargetDeath()
    {
        hasTarget = false;
        currentState = State.Idle;
    }
    //Determines if enough time has passed to attack next
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
    //The attack method for the enemy, using a mathematic parabella it makes the enemy charge to the target location and back to the original
    IEnumerator Attack()
    {
        currentState = State.Attacking;
        agent.enabled = false;

        Vector3 originalposition = transform.position;
        Vector3 dirToTarget = (target.position - transform.position).normalized;
        Vector3 attackPosition = target.position - dirToTarget * (enemyCollisionRadius);

        float attackSpeed = 2;
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

    //Enumerator used to limit the amount of times an enemy will search for a target every second.Controlled by the refreshRate variable
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
