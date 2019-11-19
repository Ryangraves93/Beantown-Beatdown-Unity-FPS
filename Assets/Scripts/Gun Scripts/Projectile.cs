using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour
{
    public LayerMask collisionMask;
    float speed = 10;
    float damage = 1;

    float offsetWidth = .1f;
    private void Start()
    {
        Collider[] intialCollisions = Physics.OverlapSphere(transform.position, .1f, collisionMask);
        if (intialCollisions.Length > 0)
        {
            OnHitObject(intialCollisions[0], transform.position);
        }
    }
    public void SetSpeed(float newSpeed) {
        speed = newSpeed;
    }
  
    void Update()
    {
        float moveDistance = speed * Time.deltaTime;
        CheckCollisions(moveDistance);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
     
    }

    void CheckCollisions(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, moveDistance + offsetWidth, collisionMask, QueryTriggerInteraction.Collide))
        {
            OnHitObject(hit.collider, hit.point);
        }
    }

    void OnHitObject (Collider c, Vector3 hitPoint)
    {
        IDamageable damageableObject = c.GetComponent<IDamageable>();
        if (damageableObject != null)
        {
            damageableObject.TakeHit(damage, hitPoint, transform.forward);
        }   
        GameObject.Destroy(gameObject);
    }
}
