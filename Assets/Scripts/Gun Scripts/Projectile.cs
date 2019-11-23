using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour
{
    //Projectile Variables
    public LayerMask collisionMask;
    float speed = 10;
    float damage = 1;
    float offsetWidth = .1f;

    
    private void Start()//On start creates an array of colliders which than calls a function which passes in the collision and it's position
    {
        Collider[] intialCollisions = Physics.OverlapSphere(transform.position, .1f, collisionMask);
        if (intialCollisions.Length > 0)
        {
            OnHitObject(intialCollisions[0], transform.position);
        }
    }
    public void SetSpeed(float newSpeed) { //Set's projectile speed
        speed = newSpeed;
    }
  
    void Update()//Consistantly check for collisions and also set's the direction the projectile
    {
        float moveDistance = speed * Time.deltaTime;
        CheckCollisions(moveDistance);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
     
    }
    void CheckCollisions(float moveDistance)//Sets ray as forward facing raycast than combines the speed and the offset
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, moveDistance + offsetWidth, collisionMask, QueryTriggerInteraction.Collide))
        {
            OnHitObject(hit.collider, hit.point);
        }
    }
    //OnHitObject is called on collisions which passes in the collisions on the point of contact. The IDamageable interface is given a reference
    //and then it passes in the information to the TakeHit Function
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
